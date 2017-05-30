using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public bool lightOn = true;

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
        GetComponentInChildren<Light>().enabled = lightOn;
    }
}
