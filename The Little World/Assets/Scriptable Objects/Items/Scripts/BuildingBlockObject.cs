using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Object", menuName = "Inventory System/Items/Block")]
public class BuildingBlockObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Block;
    }
}
