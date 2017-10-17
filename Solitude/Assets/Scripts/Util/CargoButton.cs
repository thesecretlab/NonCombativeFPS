using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The CargoButton class runs the crane animation and plays the button's sound on interacting
/// </summary>

public class CargoButton : Interactable
{

    AudioSource buttonSound;
    GameObject player;
    public CargoRoomAnim room;


    protected override void setup()
    {
        buttonSound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        buttonSound.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.5f);
    }

    public override void interact()
    {
        buttonSound.Play();
        room.playAnim();
    }

}
