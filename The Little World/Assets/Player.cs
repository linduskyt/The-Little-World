using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
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
        inventory.Container.Clear();
    }
}
