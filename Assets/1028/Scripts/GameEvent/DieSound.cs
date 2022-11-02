using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSound : MonoBehaviour
{
    private AudioSource audioPlayer;

    public void PlayerDieSound()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer?.Play();
        Debug.Log("Die sound play");
    }
}
