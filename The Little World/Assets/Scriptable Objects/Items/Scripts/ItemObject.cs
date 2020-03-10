using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of all the different item types.
/// </summary>
public enum ItemType
{
    Food,
    Equipment,
    Default,
    Block
}

/// <summary>
/// List of all the different item attributes.
/// </summary>
public enum Attributes
{
    Attack,
    Defense,
    Heal,
    Mining_Speed

}

/// <summary>
/// Base information all items require.
/// </summary>
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

/// <summary>
/// Creates the item object used in the inventory slots.
/// </summary>
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemType type;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        type = item.type;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

/// <summary>
/// Generates the item attribute values within given range.
/// </summary>
[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}