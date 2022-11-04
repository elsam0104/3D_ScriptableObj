using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType : int
{
    Head,
    Body,
    LeftHand,
    RightHand,
    Default,
}

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory System/Items/New Item")]
public class ItemObj : ScriptableObject
{
    public ItemType itemType;
    //�������°�
    public bool flagStackable;

    public Sprite itemIcon;
    public GameObject objModelPrefab;

    public List<string> boneNameLists = new List<string>();

    public Item itemData = new Item();

    [TextArea(15,20)]
    public string itemSummery;

    //���� ���� �Ǿ��� ��
    private void OnValidate()
    {
        boneNameLists.Clear();
        itemData = CreateItemObj();
    }
    public Item CreateItemObj()
    {
        Item new_item = new Item(this);
        return new_item;
    }
}
