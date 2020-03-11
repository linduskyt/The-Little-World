﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas = null;
    private MouseItem mouseItem = new MouseItem();
    [SerializeField] private GameObject inventoryPrefab = null;
    public InventoryObject inventory;

    [SerializeField] private int X_SPACE = 0;
    [SerializeField] private int X_START = 0;
    [SerializeField] private int Y_START = 0;
    [SerializeField] private int Y_SPACE = 0;
    [SerializeField] private int NUMBER_OF_COLUMNS = 0;
    private int slotId = 0;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    
    //Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    /// <summary>
    /// Updates the sprites in the player's inventory to match the database.
    /// </summary>
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
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
            mouseItem.hoverItem = itemsDisplayed[obj];
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
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(40, 40);
        mouseObject.transform.SetParent(transform.parent);
        rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];

    }

    /// <summary>
    /// Activates once when the player is no longer dragging the item slot.
    /// It deletes the copy sprite after letting go of the object.
    /// </summary>
    /// <param name="obj">Item slot which player was dragging.</param>
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj && mouseItem.item.ID >= 0)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            //inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    /// <summary>
    /// Activates when the player is dragging the item slot.
    /// It transforms the a copy sprite to the player's mouse position.
    /// </summary>
    /// <param name="obj">Item slot which player is/was dragging.</param>
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            Vector2 returnPos;
            RectTransform mouseItemTransRef = mouseItem.obj.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, mousePos, myCanvas.worldCamera, out returnPos);
            mouseItemTransRef.position = myCanvas.transform.TransformPoint(returnPos);
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
}