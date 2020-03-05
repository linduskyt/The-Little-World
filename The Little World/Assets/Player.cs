﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject hotbar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            hotbar.AddItem(new Item(item.item), 1);
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            inventory.Save();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            inventory.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[36];
        hotbar.Container.Items = new InventorySlot[9];
    }
}
