using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerWithSO : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public void SetColor(ItemSO itemColor)
    {
        Color color = itemColor.color;
        player.GetComponent<Renderer>().material.color = color;

        Debug.Log($"SetColor : {itemColor.itemColor}");
    }
}