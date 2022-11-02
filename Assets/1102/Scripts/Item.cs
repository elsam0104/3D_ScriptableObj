using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public int item_id = -1;
    public string item_name;
    public ItemAblilty[] ability;

    public Item()
    {
        item_id = -1;
        item_name = "";
    }

    public Item(ItemObj itemObj)
    {
        item_id = itemObj.itemData.item_id;
        item_name = itemObj.itemData.item_name;

        ability = new ItemAblilty[itemObj.itemData.ability.Length];

        for (int i = 0; i < ability.Length; ++i)
        {
            ability[i] = new ItemAblilty(itemObj.itemData.ability[i].Min, itemObj.itemData.ability[i].Max)
            {
                characterStack = itemObj.itemData.ability[i].characterStack
            };
        }
    }
}
