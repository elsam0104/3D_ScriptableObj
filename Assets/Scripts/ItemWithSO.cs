using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWithSO : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemSO;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControllerWithSO itemManager = other.GetComponent<PlayerControllerWithSO>();

        if(itemManager!=null)
        {
            itemManager.SetColor(itemSO);
        }
        Destroy(gameObject);
    }
}
