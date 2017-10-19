using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief Class for the levers in the breaker mini-game
/// 
public class Lever : Interactable {
	/// The animator on the lever
    Animator anim;
	/// Reference to the central breaker system
    PowerLines lines;
	/// The levers ID
    public int num;
	/// The particalSystem for the sparks
    public ParticleSystem particle;
	/// The sound source for the sparks
    AudioSource sparkSound;
	/// The sound clip for the lever
    public AudioClip switchSound;
	/// if the lever is blown or not
    bool blown;
    /// 
    /// \brief Sets the lever to the blown state
    /// 
    /// \return No return value
    /// 
    /// \details Gets the particle system to throw sparks and starts the spark sound playing
    /// 
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
    /// 
    /// \brief Gets if the lever is blown
    /// 
    /// \return Returns if the lever is blown
    /// 
    /// \details 
    /// 
    public bool isBlown() {
        sparkSound.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 0.5f);
        return blown;
    }
    /// 
    /// \brief Called when the player interacts with the lever
    /// 
    /// \return No return value
    /// 
    /// \details Inherited from the intractable class. Fixes the lever if it is blown.
    /// 
    public override void interact() {
        if (blown) {
            particle.Stop();
            sparkSound.Stop();
            active = false;
            anim.SetTrigger("pull");
            blown = false;
            lines.throwLever(num);
            sparkSound.PlayOneShot(switchSound,1.5f * PlayerPrefs.GetFloat("SFXSound"));
        }
    }
    /// 
    /// \brief Sets the powerLines object for the lever
    /// 
    /// \param [in] lines The powerLines object
    /// \param [in] num The ID of the lever
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void setLines(PowerLines lines,int num) {
        this.lines = lines;
        this.num = num;
    }
    /// 
    /// \brief Used for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Inherited form the intractable class
    /// 
    protected override void setup() {
        active = false;
        particle.Stop();
        anim = GetComponent<Animator>();
        sparkSound = GetComponent<AudioSource>();
        blown = false;
    }
}
