using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Inventory
{
    public InventorySlot[] inventorySlots = new InventorySlot[24];

    public void Clear()
    {
        foreach(InventorySlot inventorySlot in inventorySlots)
        {
            inventorySlot.DestroyItem();
        }
    }

    public bool GetFlagHave(int id)
    {
        return inventorySlots.First(i => i.item.item_id == id) != null;
    }

    public bool GetFlagHave(ItemObj itemObj)
    {
        return GetFlagHave(itemObj.itemData.item_id);
    }
}
