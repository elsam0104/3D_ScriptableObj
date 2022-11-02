using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUI : MonoBehaviour
{
    public void ShowDieText()
    {
        transform.Find("DieText")?.gameObject.SetActive(true);
        Debug.Log("Player die");
    }
}
