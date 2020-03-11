﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBlockWithInventory : MonoBehaviour
{
    [SerializeField] GameObject blockPreFab = null;
    private DisplayHotbar hotbar;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        hotbar = GameObject.FindWithTag("Hotbar").GetComponent<DisplayHotbar>();
    }

    // Update is called once per frame
    void Update()
    {
        item = hotbar.selectedSlot.item;
        if (item.type == ItemType.Block)
        {
            //If right click, drop block under mouse
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) { Debug.Log("Placable: False"); }
            if (hit.collider == null) { Debug.Log("Placable: True"); }
            if (Input.GetMouseButtonUp(1) == true && hotbar.selectedSlot.amount > 0 && hit.collider == null)
            {
                Debug.Log("Upclick");
                Debug.Log("Removing one " + item.Name);
                hotbar.selectedSlot.addAmount(-1);

                if (mousePos.x > 0.16F)
                    mousePos.x += 0.16F;
                else if (mousePos.x < -0.16F)
                    mousePos.x -= 0.16F;

                if (mousePos.y > 0.16F)
                    mousePos.y += 0.16F;
                else if (mousePos.y < -0.16F)
                    mousePos.y -= 0.16F;

                mousePos.x = mousePos.x - (mousePos.x % 0.32F);
                mousePos.y = mousePos.y - (mousePos.y % 0.32F);
                //mousePos.z = -5 + (mousePos.y * .0001F);
                mousePos.z = 0;

                Instantiate(blockPreFab, mousePos, Quaternion.identity);

            }

            if (hotbar.selectedSlot.amount <= 0)
            {
                hotbar.selectedSlot.ID = -1;
            }
        }
        //If left click, break block under mouse
        else if (Input.GetMouseButtonUp(0) == true)
        {

        }
    }
}