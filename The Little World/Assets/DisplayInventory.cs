﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int X_SPACE;
    public int X_START;
    public int Y_START;
    public int Y_SPACE;
    public int NUMBER_OF_COLUMNS;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    //void Start()
    //{
    //    CreateDisplay();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    UpdateDisplay();
    //}

    //public void CreateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.Count; i++)
    //    {
    //        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //        obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
    //        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //        itemsDisplayed.Add(inventory.Container[i], obj);
    //    }
    //}

    //public Vector3 GetPosition(int i)
    //{
    //    return new Vector3(X_START + (X_SPACE * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE * (i / NUMBER_OF_COLUMNS)), 0f);
    //}

    //public void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.Count; i++)
    //    {
    //        if(itemsDisplayed.ContainsKey(inventory.Container[i]))
    //        {
    //            itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //        }
    //        else
    //        {
    //            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
    //            itemsDisplayed.Add(inventory.Container[i], obj);
    //        }
    //    }
    //}
}
