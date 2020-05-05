using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
    [SerializeField] private bool disableInventoryResetOnStop = false;

    /// <summary>
    /// Adds the item to the player's inventory.
    /// </summary>
    /// <param name="_item">Item being added to the inventory.</param>
    /// <param name="_amount">Amount being added to the inventory.</param>
    public void AddItem(Item _item, int _amount)
    {

        if (_item.buffs.Length > 0)
        {
            setEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].addAmount(_amount);
                return;
            }
        }
        setEmptySlot(_item, _amount);
    }

    /// <summary>
    /// Searches for an open slot within the player's inventory to place item.
    /// </summary>
    /// <param name="_item">Item being added to the player's inventory.</param>
    /// <param name="_amount">Amount being added to the player's inventory.</param>
    /// <returns></returns>
    public InventorySlot setEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    /// <summary>
    /// Moves items from one slot to another.
    /// </summary>
    /// <param name="item1">Item being swapped with second item.</param>
    /// <param name="item2">Item being swapped with first item.</param>
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    /// <summary>
    /// Clears the inventory upon closing the game.
    /// </summary>
    [ContextMenu("Clear")]
    public void Clear()
    {
        if (!disableInventoryResetOnStop)
            Container = new Inventory();
    }
}

/// <summary>
/// Creates an inventory size.
/// </summary>
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[36];
}

/// <summary>
/// Contains the information of the inventory slot and manipulations of the inventory slot.
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public int slotId = -1;
    public int ID = -1;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void addAmount(int value)
    {
        amount += value;
    }
}