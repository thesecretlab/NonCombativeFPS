using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// Switches between two gameObjects (Diffrent Models) when setpower() is called.
/// Used for when the whole ship or room's power is down to turn of the lights.
///should reflect this state and be changed in another script.
///</summary>
///<remarks>
///Created By Alexander Tilley 12/08/2017
///Edited By Jeffrey Albion: Added interaction through Light Interface
///</remarks> 
public class FluroLight : MonoBehaviour, shipLight {

	///Power State Only used an initialization
	public bool power = false;		

	///Recomended to have both objects as children from the object this script is apart of
	///The Light On Object (Should contain Unity Light as child)
	public GameObject lightOn;		
	///The Light Off Object (should not have Emission map) 
	public GameObject lightOff;		


	///Intitalally loaded object
	void Start () {
		///If Both Objects are set
		if (lightOn != null && lightOff != null) {	
			if (power) {
				///Change Model
				lightOff.SetActive (false);				
				lightOn.SetActive (true);
				//Debug.Log("Flood Ligth Power On");
			} else {
				lightOn.SetActive (false);
				lightOff.SetActive (true);
				//Debug.Log("Flood Ligth Power Off");
			}
		}
	}

	/// Update is called once per frame
	void Update () {

	}

	///Switches the between the diffrent game objects
	public void setPower(int powerState){
        //Debug.Log(powerState);
		if (powerState>0) {
			lightOff.SetActive (false);
			lightOn.SetActive (true);
		} else {
			lightOn.SetActive (false);
			lightOff.SetActive (true);
		}
	}
}
