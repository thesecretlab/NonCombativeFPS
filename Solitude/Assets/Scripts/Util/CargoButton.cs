using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
