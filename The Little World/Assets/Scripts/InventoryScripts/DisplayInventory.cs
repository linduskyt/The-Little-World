using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public bool isDragging = false;
    public bool isPlacable = false;
    public bool stacking = false;

    [SerializeField] private Canvas myCanvas = null;
    private MouseItem mouseItem = new MouseItem();
    [SerializeField] private GameObject inventoryPrefab = null;
    [SerializeField] private GameObject inventoryScreen = null;

    [SerializeField] private int X_SPACE = 0;
    [SerializeField] private int X_START = 0;
    [SerializeField] private int Y_START = 0;
    [SerializeField] private int Y_SPACE = 0;
    [SerializeField] private int NUMBER_OF_COLUMNS = 0;
    [SerializeField] private bool setUnactiveAtStart;

    private int slotId = 0;
    private int itemId = 0;
    private InventorySlot tempObject;
    private TextMeshProUGUI dragItemText;

    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Awake()
    {

    }

    //Start is called before the first frame update
    void Start()
    {
        inventoryScreen = this.gameObject;
        CreateSlots();
        UpdateSlots();
        if (setUnactiveAtStart)
            inventoryScreen.SetActive(false);
        isPlacable = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        
        if (isDragging && Input.GetMouseButtonUp(0))
            isDragging = false;
        if (isDragging && Input.GetMouseButtonDown(1))
        {
            //stacking = true;
            //splitStack();
        }
    }

    /// <summary>
    /// Updates the sprites in the player's inventory to match the database.
    /// </summary>
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                if (_slot.Value.slotId >= 9)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(1, 1, 1, 0);
                }
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            if (_slot.Value.amount == 0 && _slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    /// <summary>
    /// Creates the item slots for the player's inventory and creates the events for interaction with the inventory.
    /// </summary>
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            inventory.Container.Items[i].slotId = slotId++;

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }

        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= -1)
            {
                if (_slot.Value.slotId >= 9)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(1, 1, 1, 0);
                }
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    /// <summary>
    /// Method/Function which creates an event.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type">Type of event trigger.</param>
    /// <param name="action"></param>
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    /// <summary>
    /// Active when the player hovers over the item slot.
    /// Sets the selected item to whatever is being hovered over.
    /// </summary>
    /// <param name="obj">Item slot which player is hovering over.</param>
    public void OnEnter(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color32(169, 169, 169, 100);
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemsDisplayed[obj];
        }
    }

    /// <summary>
    /// Activates once when the player is no longer hovering over the item slot.
    /// Resets the selected item.
    /// </summary>
    /// <param name="obj">Item slot which player was hovering over.</param>
    public void OnExit(GameObject obj)
    {

        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }

    /// <summary>
    /// Activates once the player begins to drag the item slot.
    /// It creates a copy sprite of the item which is being dragged.
    /// </summary>
    /// <param name="obj">Item slot which player is dragging.</param>
    public void OnDragStart(GameObject obj)
    {
        Debug.Log("Start Dragging: " + isDragging);
        if (mouseItem.hoverItem != null)
        {
            if (Input.GetMouseButton(0) && !(Input.GetMouseButton(1)) && mouseItem.hoverItem.ID >= 0 && mouseItem.hoverItem.amount > 0)
            {
                isDragging = true;
                var mouseObject = new GameObject();
                var rt = mouseObject.AddComponent<RectTransform>();
                var mouseChild = new GameObject();
                mouseChild.transform.Translate((float)25, -(float)25, 0);
                dragItemText = mouseChild.AddComponent<TextMeshProUGUI>();
                mouseChild.transform.SetParent(mouseObject.transform);


                rt.sizeDelta = new Vector2(70, 70);

                dragItemText.fontSize = 45;
                dragItemText.autoSizeTextContainer = true;
                dragItemText.fontStyle = FontStyles.Bold;
                dragItemText.alignment = TextAlignmentOptions.MidlineJustified;
                dragItemText.outlineWidth = (float)0.3;
                dragItemText.outlineColor = Color.black;

                mouseObject.transform.SetParent(transform.parent);
                rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (itemsDisplayed[obj].ID >= 0 && isDragging)
                {
                    var img = mouseObject.AddComponent<Image>();
                    img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
                    if (itemsDisplayed[obj].amount > 1)
                        dragItemText.text = itemsDisplayed[obj].amount.ToString("n0");
                    else
                        dragItemText.text = "";
                    img.raycastTarget = false;
                }
                tempObject = itemsDisplayed[obj];
                mouseItem.obj = mouseObject;
                mouseItem.item = itemsDisplayed[obj];
                itemId = itemsDisplayed[obj].ID;
                itemsDisplayed[obj].ID = -1;
            }
        }
    }

    /// <summary>
    /// Activates once when the player is no longer dragging the item slot.
    /// It deletes the copy sprite after letting go of the object.
    /// </summary>
    /// <param name="obj">Item slot which player was dragging.</param>
    public void OnDragEnd(GameObject obj)
    {
        Debug.Log("End Dragging: " + isDragging);
        if (mouseItem.hoverItem != null && isDragging)
        {
            if (mouseItem.hoverItem.ID == mouseItem.item.item.Id && itemsDisplayed[obj].slotId != mouseItem.hoverItem.slotId)
            {
                Debug.Log("End Drag Case 1");
                Debug.Log("itemsDisplayed[obj].slotId: " + itemsDisplayed[obj].slotId);
                Debug.Log("mouseItem.hoverItem.slotId: " + mouseItem.hoverItem.slotId);
                itemsDisplayed[obj].ID = itemId;
                itemsDisplayed[obj].item.Id = itemId;
                mouseItem.hoverItem.amount = mouseItem.hoverItem.amount + mouseItem.item.amount;
                mouseItem.item.ID = -1;
            }
            else if (mouseItem.hoverItem.ID == mouseItem.item.item.Id && mouseItem.hoverItem.slotId == mouseItem.item.slotId)
            {
                Debug.Log("End Drag Case 2");
                Debug.Log("itemsDisplayed[obj].slotId: " + itemsDisplayed[obj].slotId);
                Debug.Log("mouseItem.hoverItem.slotId: " + mouseItem.hoverItem.slotId);
                itemsDisplayed[obj].ID = itemId;
                itemsDisplayed[obj].item.Id = itemId;
                mouseItem.hoverItem.amount = mouseItem.hoverItem.amount + mouseItem.item.amount;
            }
            else if (mouseItem.hoverItem.ID >= 0)
            {
                Debug.Log("End Drag Case 3");
                itemsDisplayed[obj].ID = itemId;
                itemsDisplayed[obj].item.Id = itemId;
                inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
            }
            else if (mouseItem.hoverItem.ID == -1)
            {
                Debug.Log("End Drag Case 4");
                Debug.Log("mouseItem.item.ID: " + itemId);
                inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
                itemsDisplayed[mouseItem.hoverObj].ID = itemId;
            }
            else
            {
                Debug.Log("End Drag Case 5.1");
                Debug.Log("Recovering Item");
                itemsDisplayed[obj].ID = itemId;
                itemsDisplayed[obj].item.Id = itemId;
                if (stacking)
                {
                    mouseItem.item.amount += mouseItem.item.amount;
                }
                //itemsDisplayed[obj] = tempObject;
            }
        }
        else
        {
            if (mouseItem.hoverItem != null && isDragging)
            {
                if (mouseItem.item.ID >= 0 && mouseItem.hoverItem.ID >= 0)
                {
                    Debug.Log("End Drag Case 5.2");
                    Debug.Log("Recovering Item");
                    itemsDisplayed[obj].ID = itemId;
                    itemsDisplayed[obj].item.Id = itemId;
                    if (stacking)
                    {
                        itemsDisplayed[obj].amount += mouseItem.item.amount;
                    }
                }
            }
            else if (isDragging)
            {
                itemsDisplayed[obj].ID = itemId;
                itemsDisplayed[obj].item.Id = itemId;
                if (stacking)
                {
                    itemsDisplayed[obj].amount += mouseItem.item.amount;
                }
            }
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
        isDragging = false;
        stacking = false;
        Debug.Log("Is Dragging: " + isDragging);
    }

    /// <summary>
    /// Activates when the player is dragging the item slot.
    /// It transforms the a copy sprite to the player's mouse position.
    /// </summary>
    /// <param name="obj">Item slot which player is/was dragging.</param>
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null && isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            Vector2 returnPos;
            RectTransform mouseItemTransRef = mouseItem.obj.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, mousePos, myCanvas.worldCamera, out returnPos);
            mouseItemTransRef.position = myCanvas.transform.TransformPoint(returnPos);
            
            if (dragItemText.text == "1")
            {
                dragItemText.text = "";
                mouseItem.item.amount = 1;
            }
            if (dragItemText.text != "")
                if (Int32.Parse(dragItemText.text) != mouseItem.item.amount)
                    mouseItem.item.amount = Int32.Parse(dragItemText.text);
        }
    }

    /// <summary>
    /// Generates the position which the item slots need to be generated at from given inital points.
    /// </summary>
    /// <param name="i">Slot number in row.</param>
    /// <returns></returns>
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    private void splitStack()
    {
        if (mouseItem.hoverItem != null && isDragging)
        {
            int stackAmount = mouseItem.item.amount;
            Debug.Log("Stack size: " + stackAmount);
            mouseItem.item.amount = 0;
            Debug.Log("Hover item ID: " + mouseItem.hoverItem.item.Id);
            if (stackAmount == 1 && mouseItem.hoverItem.slotId != mouseItem.item.slotId)
            {
                Debug.Log("Stack = 1");
                mouseItem.item.ID = itemId;
                dragItemText.text = (0).ToString();
                stackAmount = 0;
                mouseItem.hoverItem.UpdateSlot(itemId, mouseItem.item.item, 1);
                Destroy(mouseItem.obj);
                isDragging = false;
            }
            else if (mouseItem.hoverItem.slotId == mouseItem.item.slotId && stackAmount == 1)
            {
                Debug.Log("Case: Same Slot with Stack = 1");
                mouseItem.item.ID = itemId;
                dragItemText.text = (0).ToString();
                stackAmount = 0;
                mouseItem.hoverItem.addAmount(1);
                //mouseItem.hoverItem.UpdateSlot(itemId, mouseItem.item.item, 1);
                Destroy(mouseItem.obj);
                isDragging = false;
            }
            else if (mouseItem.hoverItem.slotId == mouseItem.item.slotId && stackAmount > 1)
            {
                Debug.Log("Case: Same slot as mouse item");
                mouseItem.item.ID = itemId;
                dragItemText.text = (stackAmount - (Mathf.CeilToInt((float)1.0 * stackAmount / 2))).ToString();
                mouseItem.hoverItem.addAmount(Mathf.CeilToInt((float)1.0 * stackAmount / 2));
                //mouseItem.hoverItem.UpdateSlot(mouseItem.item.item.Id, mouseItem.item.item, Mathf.FloorToInt((float)1.0 * stackAmount / 2));
            }

            else if (itemsDisplayed[mouseItem.hoverObj].ID == -1 && mouseItem != null && itemsDisplayed[mouseItem.hoverObj].item.Id >= 0 && stackAmount > 0)
            {
                Debug.Log("Case: Different slot to mouse item");
                //itemsDisplayed[mouseItem.hoverObj].item.Id = mouseItem.item.ID;
                //itemsDisplayed[mouseItem.hoverObj].amount = Mathf.FloorToInt((float)1.0 * mouseItem.item.amount / 2);
                itemsDisplayed[mouseItem.hoverObj].ID = itemId;
                dragItemText.text = (stackAmount - (Mathf.CeilToInt((float)1.0 * stackAmount / 2))).ToString();
                mouseItem.hoverItem.UpdateSlot(mouseItem.item.item.Id, mouseItem.item.item, Mathf.CeilToInt((float)1.0 * stackAmount / 2));
            }
        }
    }

    public void mouseEnter()
    {
        isPlacable = false;
    }

    public void mouseExit()
    {
        isPlacable = true;
    }
}

/// <summary>
/// The object which the mouse is hovering over.
/// </summary>
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
    public string itemAmount;
}