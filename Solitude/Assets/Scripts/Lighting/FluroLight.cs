using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Switches between two gameObjects (Diffrent Models) by boolean value.
 * 
 * If the whole ship or room the light is in power is down then var power
 * should reflect this state and be changed in another script.
 * 
 * Created By Alexander Tilley 12/08/2017
 */

public class FluroLight : MonoBehaviour {

	public bool power = false;		//Power State Only used an initialization

	//Recomended to have both objects as children from the object this script is apart of

	public GameObject lightOn;		//The Light On Object (Should contain Unity Light as child)
	public GameObject lightOff;		//The Light Off Object (should not have Emission map) 


	//Intitalally loaded object
	void Start () {
		if (lightOn != null && lightOff != null) {	//If Both Objects are set
			if (power) {
				lightOff.SetActive (false);				//Change Model
				lightOn.SetActive (true);
				//Debug.Log("Flood Ligth Power On");
			} else {
				lightOn.SetActive (false);
				lightOff.SetActive (true);
				//Debug.Log("Flood Ligth Power Off");
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}

	//Switches the between the diffrent game objects
	public void setPower(bool powerState){
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
