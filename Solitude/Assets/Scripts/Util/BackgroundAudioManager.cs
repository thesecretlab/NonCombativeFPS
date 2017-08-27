using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {

    public AudioSource BGSound;
	
	// Update is called once per frame
	void Update () {
        BGSound.volume = ((PlayerPrefs.GetFloat("BGSound")) * 0.2f);
    }
}
