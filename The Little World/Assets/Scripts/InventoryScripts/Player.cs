using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject hotbar;
    private PolygonCollider2D col;

    /// <summary>
    /// Adds item to player inventory and destroys collided item.
    /// </summary>
    /// <param name="collision">Item being collided with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), item.amount);
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

    /// <summary>
    /// Sets inventory and hotbar sizes on application quit.
    /// </summary>
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[36];
        hotbar.Container.Items = new InventorySlot[9];
    }
}
