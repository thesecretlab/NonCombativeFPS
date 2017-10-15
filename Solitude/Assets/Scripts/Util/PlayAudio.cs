using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not currently used
/// </summary>

public class PlayAudio : MonoBehaviour
{

    public AudioSource toPlay;

    // Use this for initialization
    void playSound()
    {
        toPlay.Play();
    }
}