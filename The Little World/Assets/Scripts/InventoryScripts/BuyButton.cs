using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private DisplayHotbar npcShop = null;
    [SerializeField] private DisplayInventory playerInventory = null;
    [SerializeField] private TextMeshProUGUI buyText = null;
    [SerializeField] private Player player = null;

    public int buyAmount;

    // Start is called before the first frame update
    void Start()
    {
        buyText.text = "0";
        buyAmount = 0;
        if (npcShop == null)
            npcShop = GameObject.Find("NPCShopPanel").GetComponent<DisplayHotbar>();
        if (playerInventory == null)
            playerInventory = GameObject.Find("PlayerInventory").GetComponent<DisplayInventory>();
        if (buyText == null)
            buyText = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();
        if (player == null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the buy amount
        if (Int32.Parse(buyText.text) != buyAmount)
        {
            buyText.text = buyAmount.ToString();
        }
    }

    public void buyItem()
    {
        bool itemExistsInInventory = false;
        //    if (npcShop.selectedSlot.slotId != -1)
        //        player.inventory.AddItem(npcShop.selectedSlot.item, npcShop.selectedSlot.amount * buyAmount);
        //    player.GetComponent<DisplayInventory>().UpdateSlots();
        int buyTotal = buyAmount * npcShop.selectedSlot.amount;
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in playerInventory.itemsDisplayed)
        {
            if (_slot.Value.item.Id == npcShop.selectedSlot.item.Id)
            {
                itemExistsInInventory = true;
                _slot.Value.amount += buyTotal;
            }
        }
        if (!itemExistsInInventory && npcShop.selectedSlot.slotId >= 0)
        {
            player.inventory.AddItem(npcShop.selectedSlot.item, buyTotal);
        }

    }

    public void addBuyAmount(int amount)
    {
        buyAmount += amount;
        if (buyAmount <= 0)
            buyAmount = 0;
    }
}
