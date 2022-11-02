using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySlot
{
    //slot에 들어갈 아이템 타입
    public ItemType[] itemTypes = new ItemType[0];

    [NonSerialized]
    public InventoryObj inventoryObj;
    [NonSerialized]
    public GameObject slotUI;

    [NonSerialized]
    public Action<InventorySlot> OnPreUpload;
    [NonSerialized]
    public Action<InventorySlot> OnPostUpload;

    public Item item;
    public int itemCnt;

    public ItemObj ItemObject
    {
        get
        {
            return item.item_id >= 0 ? inventoryObj.itemDBObj.itemObjects[item.item_id] : null;
        }
    }

    public InventorySlot() => UploadSlot(new Item(), 0);
    public InventorySlot(Item item, int cnt) => UploadSlot(item, cnt);

    public void DestroyItem() => UploadSlot(new Item(), 0);
    public void AddCount(int value) => UploadSlot(item, itemCnt += value);
    public void AddItem(Item item, int cnt) => UploadSlot(item, cnt);


    public void UploadSlot(Item item, int cnt)
    {
        if (cnt <= 0)
        {
            item = new Item();
        }

        OnPreUpload?.Invoke(this);
        this.item = item;
        this.itemCnt = cnt;
        OnPostUpload?.Invoke(this);
    }

    public bool GetFlagEquipSlot(ItemObj itemObj)
    {
        if (itemTypes.Length <= 0 || item.item_id < 0 || itemObj == null)
            return true;
        foreach (ItemType itemType in itemTypes)
        {
            if (itemType == itemObj.itemType)
                return true;
        }
        return false;
    }
}
