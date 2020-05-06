using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    [SerializeField] private DisplayHotbar npcShop = null;
    [SerializeField] private DisplayInventory playerInventory = null;
    [SerializeField] private TextMeshProUGUI sellText = null;
    [SerializeField] private Player player = null;

    public int sellAmount;

    // Start is called before the first frame update
    void Start()
    {
        sellText.text = "0";
        sellAmount = 0;
        if (npcShop == null)
            npcShop = GameObject.Find("NPCShopPanel").GetComponent<DisplayHotbar>();
        if (playerInventory == null)
            playerInventory = GameObject.Find("PlayerInventory").GetComponent<DisplayInventory>();
        if (sellText == null)
            sellText = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();
        if (player == null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the buy amount
        if (sellAmount != Int32.Parse(sellText.text))
        {
            sellAmount = Int32.Parse(sellText.text);
        }
    }

    public void sellItem()
    {
        int sellTotal = sellAmount * npcShop.selectedSlot.amount;
        Debug.Log("Sell Amount: " + sellAmount + "\nNPC Shop Base Amount: " + npcShop.selectedSlot.amount + "\nSelling Item");
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in playerInventory.itemsDisplayed)
        {
            if (_slot.Value.item.Id == npcShop.selectedSlot.item.Id)
            {
                Debug.Log("Inventory Slot: " + _slot.Value.slotId);
                if (sellTotal >= _slot.Value.amount)
                {
                    sellTotal = _slot.Value.amount;


                    if (_slot.Value.amount == 0 && _slot.Value.ID >= 0)
                    {
                        _slot.Value.ID = -1;
                        _slot.Value.item = null;
                    }
                }
                _slot.Value.amount -= sellTotal;
                playerInventory.UpdateSlots();
            }
        }
    }
}
