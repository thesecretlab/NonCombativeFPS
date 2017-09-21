using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoRoomAnim : MonoBehaviour
{

    Animator anim;
    int nextCrate;
    AudioSource source;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.7f);
    }

    public void playAnim()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Default State"))
        {
            nextCrate = Random.Range(1, 4);
            switch (nextCrate)
            {
                case 1:
                    anim.SetTrigger("Crate1Fetch");
                    Debug.Log("crate1");
                    break;
                case 2:
                    anim.SetTrigger("Crate2Fetch");
                    Debug.Log("crate2");
                    break;
                case 3:
                    anim.SetTrigger("Crate3Fetch");
                    Debug.Log("crate3");
                    break;
                case 4:
                    anim.SetTrigger("Crate4Fetch");
                    Debug.Log("crate4");
                    break;
            }
        }
        else if (
            anim.GetCurrentAnimatorStateInfo(0).IsName("Crate 1 Resting") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Crate 2 Resting") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Crate 3 Resting") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Crate 4 Resting")
            )
        {
            switch (nextCrate)
            {
                case 1:
                    anim.SetTrigger("Crate1Return");
                    break;
                case 2:
                    anim.SetTrigger("Crate2Return");
                    break;
                case 3:
                    anim.SetTrigger("Crate3Return");
                    break;
                case 4:
                    anim.SetTrigger("Crate4Return");
                    break;
            }
        }
    } //end of playanim();
}