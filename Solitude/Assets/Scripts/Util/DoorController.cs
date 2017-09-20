using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    Animator anim;
    bool open;
    GameObject player;
    AudioSource doorNoise;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        open = false;
        player = GameObject.FindGameObjectWithTag("Player");
        doorNoise = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        doorNoise.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.5f);
        if (Vector3.Distance(player.transform.position, transform.position) < 6) //player is near door
        {
            //open doors
            if (!open)
            {
                anim.SetTrigger("Open");
                doorNoise.Play();
                open = true;
            }
        }

        else //player is not near door
        {
            if (open)
            {
                //close doors
                anim.SetTrigger("Close");
                doorNoise.Play();
                open = false;
            }
        }

	}
}
