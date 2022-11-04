using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MouseTransformData
{
    // UI �κ��丮�� ��ġ�� �ִ°�?
    public static UIInventory mouseInventory;
    // ���콺�� �巡�׾ص�� ���ΰ�?
    public static GameObject mouseDragging;
    // Slot UI�� ��ġ�� �ִ°�?
    public static GameObject mouseSlot;
}
[RequireComponent(typeof(EventTrigger))]
public abstract class UIInventory : MonoBehaviour
{
    public InventoryObj inventoryObj;
    private InventoryObj beforeInventoryObj;

    public Dictionary<GameObject, InventorySlot> uiSLotLists = new Dictionary<GameObject, InventorySlot>();
    private void Awake()
    {
        CreateUISlots();

        for (int i = 0; i < inventoryObj.inventorySlots.Length; i++)
        {
            inventoryObj.inventorySlots[i].inventoryObj = inventoryObj;
            inventoryObj.inventorySlots[i].OnPostUpload += OnEquipUpdate;
        }

        AddEvnetAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInventory(gameObject); });
        AddEvnetAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitInventory(gameObject); });
    }
    private void Start()
    {
        for (int i = 0; i < inventoryObj.inventorySlots.Length; i++)
        {
            inventoryObj.inventorySlots[i].UploadSlot(inventoryObj.inventorySlots[i].item,inventoryObj.inventorySlots[i].itemCnt);
        }
    }
    public abstract void CreateUISlots();

    /// <summary>
    /// �̺�Ʈ Ʈ���Ÿ� �߰������ִ� �Լ�
    /// </summary>
    /// <param name="gameObj">�׼��� �߰��� ������Ʈ</param>
    /// <param name="eventTriggerType">�׼��� �ߵ��� ����</param>
    /// <param name="baseEventDataAction">�ߵ��� �׼�</param>
    protected void AddEvnetAction(GameObject gameObj, EventTriggerType eventTriggerType, UnityAction<BaseEventData> baseEventDataAction)
    {
        EventTrigger eventTrigger = gameObj.GetComponent<EventTrigger>();
        Debug.Assert(eventTrigger != null, "Event trigger can not be find");
        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry { eventID = eventTriggerType };

        eventTriggerEntry.callback.AddListener(baseEventDataAction);

        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public void OnEquipUpdate(InventorySlot inventorySlot)
    {
        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().sprite =
            inventorySlot.item.item_id < 0 ? null : inventorySlot.ItemObject.itemIcon;

        inventorySlot.slotUI.transform.GetChild(0).GetComponent<Image>().color =
            inventorySlot.item.item_id < 0 ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);

        inventorySlot.slotUI.GetComponentInChildren<TextMeshProUGUI>().text =
            inventorySlot.item.item_id < 0 ? string.Empty : (inventorySlot.itemCnt == 1 ?
            string.Empty : inventorySlot.itemCnt.ToString("n0"));
    }

    public void OnEnterInventory(GameObject gameObj)
    {
        MouseTransformData.mouseInventory = gameObj.GetComponent<UIInventory>();
    }
    public void OnExitInventory(GameObject gameObj)
    {
        MouseTransformData.mouseInventory = null;
    }

    public void OnEnterSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = gameObj;
        MouseTransformData.mouseInventory = gameObj.GetComponentInParent<UIInventory>();
    }
    public void OnExitSlots(GameObject gameObj)
    {
        MouseTransformData.mouseSlot = null;
    }

    public void OnStargDrag(GameObject gameObj)
    {
        MouseTransformData.mouseDragging = AddEventDragImage(gameObj);
    }
    public void OnMoveingDrag(GameObject gameObj)
    {
        if (MouseTransformData.mouseDragging == null) return;

        MouseTransformData.mouseDragging.GetComponent<RectTransform>().position =
            Input.mousePosition;
    }
    public void OnEndDrag(GameObject gameObj)
    {
        Destroy(MouseTransformData.mouseDragging);

        if(MouseTransformData.mouseInventory == null)//������ ������
        {
            uiSLotLists[gameObj].DestroyItem();   
        }
        else if (MouseTransformData.mouseSlot)//������ ��ȯ
        {
            InventorySlot mouseHoverSlotData = MouseTransformData.mouseInventory.uiSLotLists[MouseTransformData.mouseSlot];

            inventoryObj.SwapItems(uiSLotLists[gameObj], mouseHoverSlotData);
        }
    }

    private  GameObject AddEventDragImage(GameObject gameObj)
    {
        if(uiSLotLists.ContainsKey(gameObj) == false ||uiSLotLists[gameObj].item.item_id<0)
        {
            return null;
        }

        GameObject imgDrags = new GameObject();

        RectTransform rectTransform = imgDrags.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        imgDrags.transform.SetParent(transform.parent);

        Image image = imgDrags.AddComponent<Image>();
        image.sprite = uiSLotLists[gameObj].ItemObject.itemIcon;
        image.raycastTarget = false;

        imgDrags.name = "Drag Image";

        return imgDrags;
    }
}
