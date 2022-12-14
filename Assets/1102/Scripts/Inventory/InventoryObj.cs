using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum InterfaceType
{
    Inventory,
    Equipment,
    QuickSlot,
    Bag,
}

[CreateAssetMenu(fileName ="New InventoryObj",menuName ="Inventory System/Inventory/InventoryObj")]
public class InventoryObj :ScriptableObject
{
    public ItemDBObj itemDBObj;
    public InterfaceType type;

    [SerializeField]
    private Inventory inventory = new Inventory();
    public InventorySlot[] inventorySlots => inventory.inventorySlots;

    public int GetEmptySlotCount
    {
        get
        {
            int cnt = 0;
            foreach(InventorySlot slot in inventorySlots)
            {
                if (slot.item.item_id <= -1)
                    cnt++;
            }
            return cnt;
        }
    }

    public bool AddItem (Item item, int amount)
    {
        InventorySlot invenSlot =SearchItemInInven(item);
        //장착가능한 슬롯의 갯수가 얼마인가
        if(!itemDBObj.itemObjects[item.item_id].flagStackable || invenSlot ==null)
        {
            if (GetEmptySlotCount <= 0)
                return false;
            GetEmptySlot().AddItem(item, amount);
        }
        else //중복 가능하면 수만 증가
        {
            invenSlot.AddCount(amount);
        }
        return true;
    }

    public InventorySlot SearchItemInInven(Item item)
    {
        return inventorySlots.FirstOrDefault(i => i.item.item_id == item.item_id);
    }

    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.FirstOrDefault(i => i.item.item_id <= -1);
    }

    public bool IsContainItem(ItemObj itemObj)
    {
        return inventorySlots.FirstOrDefault(i => i.item.item_id ==itemObj.itemData.item_id)!=null;
    }

    public void SwapItems(InventorySlot itemA,InventorySlot itemB)
    {
        if (itemA == itemB)
            return;

        if(itemA.GetFlagEquipSlot(itemB.ItemObject) && itemB.GetFlagEquipSlot(itemA.ItemObject))
        {
            InventorySlot temp = new InventorySlot(itemB.item, itemB.itemCnt);
            itemB.UploadSlot(itemA.item, itemA.itemCnt);
            itemA.UploadSlot(temp.item, temp.itemCnt);
        }
    }

    public void Clear()
    {
        inventory.Clear();
    }

    //사용한 아이템 효과 발동 함수
    public Action<ItemObj> OnUSeItemObject;

}