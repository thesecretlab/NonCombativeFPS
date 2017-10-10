using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {
    Animator anim;

    PowerLines rec;
    public int num;

    public ParticleSystem particle;
    AudioSource sparkSound;
    public AudioClip switchSound;

    bool blown;

    public string getName() {
        return gameObject.name;
    }

    public void blow() {
        if (!blown) {
            sparkSound.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.3f);
            particle.Play();
            sparkSound.Play();
            anim.SetTrigger("blow");
            blown = true;
            active = true;
        }
    }

    public bool isBlown() {
        sparkSound.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.5f);
        return blown;
    }

    public override void interact() {
        if (blown) {
            particle.Stop();
            sparkSound.Stop();
            active = false;
            anim.SetTrigger("pull");
            blown = false;
            rec.throwLever(num);
            sparkSound.PlayOneShot(switchSound,1.5f * PlayerPrefs.GetFloat("SFXSound"));
        }
    }

    public void setReactor(PowerLines rec,int num) {
        this.rec = rec;
        this.num = num;
    }

    protected override void setup() {
        active = false;
        particle.Stop();
        anim = GetComponent<Animator>();
        sparkSound = GetComponent<AudioSource>();
        blown = false;
    }
}
