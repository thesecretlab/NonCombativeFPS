using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodLight : MonoBehaviour {

	public bool power = false;
	public GameObject lightOn;
	public GameObject lightOff;


	// Use this for initialization
	void Start () {
		if (lightOn != null && lightOff != null) {
			if (power) {
				lightOff.SetActive (false);
				lightOn.SetActive (true);
				Debug.Log("Flood Ligth Power On");
			} else {
				lightOn.SetActive (false);
				lightOff.SetActive (true);
				Debug.Log("Flood Ligth Power Off");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setPower(bool powerState){
		power = powerState;
		if (power) {
			lightOff.SetActive (false);
			lightOn.SetActive (true);
		} else {
			lightOn.SetActive (false);
			lightOff.SetActive (true);
		}
	}
}
