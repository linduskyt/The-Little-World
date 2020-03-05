using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayHotbar : MonoBehaviour
{
    [SerializeField]
    private DisplayInventory myInventory = null;
    //[SerializeField]
    //public Sprite sel_Sprite;

    //public Image border;
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    //public Button myButton;

    public int X_SPACE;
    public int X_START;
    public int Y_START;
    public int Y_SPACE;
    public int NUMBER_OF_COLUMNS;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //Start is called before the first frame update
    void Start()
    {
        //myButton = GameObject.FindWithTag("HotbarButton").GetComponent<Button>();
        //Button btn = myButton.GetComponent<Button>();
        //border = GetComponent<Image>();
        //btn.onClick.AddListener(onClick);
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    //private void onClick()
    //{
    //    border.sprite = sel_Sprite;
    //}

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = myInventory.inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
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

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            itemsDisplayed.Add(obj, myInventory.inventory.Container.Items[i]);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE * (i / NUMBER_OF_COLUMNS)), 0f);
    }
}