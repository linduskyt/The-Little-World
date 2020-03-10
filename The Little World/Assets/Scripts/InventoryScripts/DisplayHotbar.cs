using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Linq;

public class DisplayHotbar : MonoBehaviour
{
    public InventorySlot selectedSlot = null;

    public DisplayInventory myInventory = null;
    [SerializeField] private GameObject inventoryPrefab = null;
    [SerializeField] private InventoryObject inventory = null;
    [SerializeField] private TabGroup tabGroup = null;
    private Sprite[] borderSprite;

    [SerializeField] private int X_SPACE = 0;
    [SerializeField] private int X_START = 0;
    [SerializeField] private int Y_START = 0;
    [SerializeField] private int Y_SPACE = 0;
    [SerializeField] private int NUMBER_OF_COLUMNS = 0;
    private int selectedSlotId = -1;
    private int slotId = 0;

    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //Start is called before the first frame update
    void Start()
    {
        borderSprite = Resources.LoadAll<Sprite>("Images/HotbarBlock");
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    /// <summary>
    /// Updates the item slots to match the inventory's first row.
    /// </summary>
    public void UpdateSlots()
    {
        /* Lag issue may occur because of this for loop
         * In that case, there needs to be a new method to create a 
         * hotbar containing the items in the first row of the player's inventory
         */
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i] = myInventory.inventory.Container.Items[i];
        }

        /* Changes the sprite of hotbar's slot[i] to match the sprite of the inventory's slot[i]
         * Or, it removes the sprite if there is no longer any item in the slot.
         */
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = myInventory.inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
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
    /// Creates the item slots in the hotbar.
    /// </summary>
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            itemsDisplayed.Add(obj, myInventory.inventory.Container.Items[i]);
            inventory.Container.Items[i].slotId = slotId++;
            TabButton tabButtonRef = obj.GetComponent<TabButton>();
            tabButtonRef.MyTabGroup = tabGroup;


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

    /// <summary>
    /// Changes sprite of hotbar borders to indicate hovering and selection.
    /// </summary>
    /// <param name="id">ID of the button</param>
    public void slotDisplay(int id)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.slotId != selectedSlotId)
            {
                if (id == -1)
                {
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().sprite = borderSprite[0];
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 155, 132);
                }

                else if (_slot.Value.slotId == id)
                {
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().sprite = borderSprite[1];
                    _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 155, 132);
                }
            }
        }
    }

    public void slotSelected(int id)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.slotId == id)
            {
                _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().sprite = borderSprite[1];
                _slot.Key.transform.GetChild(2).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(71, 255, 110, 132);
                selectedSlotId = id;
                selectedSlot = _slot.Value;
                Debug.Log("Selected Slot ID: " + selectedSlot.slotId);
                Debug.Log("Selected Slot Item: " + selectedSlot.item.Name);
                Debug.Log("Selected Slot Item ID: " + selectedSlot.item.Id);
                slotDisplay(-1);
            }
        }
    }
}