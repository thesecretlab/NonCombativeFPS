using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public bool lightOn = true;
    public AudioSource lightSound;


	// Use this for initialization
	void Start () {
        toggleFlashLight();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F))
        {
            toggleFlashLight();
        }
	}

    void toggleFlashLight()
    {
        lightOn = !lightOn;
        lightSound.Play();
        GetComponentInChildren<Light>().enabled = lightOn;
    }
}
