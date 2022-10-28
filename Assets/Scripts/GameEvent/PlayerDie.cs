using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField]
    GameEvent dieEvent;

    public bool doDie;
    private void Update()
    {
        if(doDie)
        {
            dieEvent.Raise();
            doDie = false;
        }
    }
}
