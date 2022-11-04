using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public InventoryObj inventoryObject = null;
    public ItemDBObj databaseObject = null;
    public void AddNewItem()
    {
        if (databaseObject.itemObjects.Length > 0)
        {
            ItemObj newItemObject = databaseObject.itemObjects[Random.Range(0, databaseObject.itemObjects.Length)];

            Item newItem = new Item(newItemObject);

            inventoryObject.AddItem(newItem, 1);
        }
    }
    public void ClearInventory()
    {
        inventoryObject?.Clear();
    }
}
