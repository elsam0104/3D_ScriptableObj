using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicUIInventory : UIInventory
{
    [SerializeField]
    protected GameObject prefabSlot;
    [SerializeField]
    protected Vector2 start;
    [SerializeField]
    protected Vector2 size;
    [SerializeField]
    protected Vector2 space;

    protected int numCols =4;

    public override void CreateUISlots()
    {
        uiSLotLists = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventoryObj.inventorySlots.Length; i++)
        {
            GameObject gameObject = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity, transform);

            gameObject.GetComponent<RectTransform>().anchoredPosition = CalculatePosition(i);

            AddEvnetAction(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterSlots(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.PointerExit, delegate { OnExitSlots(gameObject); });

            AddEvnetAction(gameObject, EventTriggerType.BeginDrag, delegate { OnStargDrag(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.Drag, delegate { OnMoveingDrag(gameObject); });
            AddEvnetAction(gameObject, EventTriggerType.EndDrag, delegate { OnEndDrag(gameObject); });

            inventoryObj.inventorySlots[i].slotUI = gameObject;
            uiSLotLists.Add(gameObject, inventoryObj.inventorySlots[i]);
            gameObject.name += ": " + i;
        }
    }

    public Vector3 CalculatePosition(int i)
    {
        float x = start.x + ((space.x + size.x) * (i % numCols));
        float y = start.y + (-(space.y+ size.y) * (i / numCols));

        return new Vector3(x, y, 0f);
    }
}