using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticUIInventory : UIInventory
{
    public GameObject[] staticSlot = null;
    public override void CreateUISlots()
    {
        uiSLotLists = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventoryObj.inventorySlots.Length; i++)
        {
            GameObject gameObject = staticSlot[i];

            AddEvnetAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObject); });

            AddEvnetAction(gameObject, EventTriggerType.BeginDrag, delegate { OnStargDrag(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.Drag, delegate { OnMoveingDrag(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObject); });

            inventoryObj.inventorySlots[i].slotUI = gameObject;
            uiSLotLists.Add(gameObject, inventoryObj.inventorySlots[i]);
        }
    }
}
