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
		if (Input.GetKeyDown(KeyCode.E))
        {
            toggleFlashLight();
        }
	}

    void toggleFlashLight()
    {
        lightOn = !lightOn;
        transform.GetChild(0).gameObject.SetActive(lightOn);
    }
}
