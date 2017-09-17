using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoRoomAnim : MonoBehaviour
{

    Animator anim;
    bool returnCrateNext; //keeps track of if the next action is to return a crate
    GameObject player;
    int nextCrate;
    AudioSource source;
    public AudioClip winchsound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        returnCrateNext = false;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.7f);
    }

    public void playAnim()
    {
        if (!returnCrateNext)
        {
            nextCrate = Random.Range(1, 4);
            switch (nextCrate)
            {
                case 1:
                    anim.SetTrigger("Crate1Fetch");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 2:
                    anim.SetTrigger("Crate2Fetch");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 3:
                    anim.SetTrigger("Crate3Fetch");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 4:
                    anim.SetTrigger("Crate4Fetch");
                    returnCrateNext = !returnCrateNext;
                    break;
            }
        }
        else
        {
            switch (nextCrate)
            {
                case 1:
                    anim.SetTrigger("Crate1Return");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 2:
                    anim.SetTrigger("Crate2Return");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 3:
                    anim.SetTrigger("Crate3Return");
                    returnCrateNext = !returnCrateNext;
                    break;
                case 4:
                    anim.SetTrigger("Crate4Return");
                    returnCrateNext = !returnCrateNext;
                    break;
            }

        }

    } //end of playanim();
}