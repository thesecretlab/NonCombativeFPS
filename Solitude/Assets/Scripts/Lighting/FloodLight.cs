using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 ///<summary>
/// Switches between two gameObjects (Diffrent Models) when setpower() is called.
 /// Used for when the whole ship or room's power is down to turn of the lights.
 ///should reflect this state and be changed in another script.
 ///</summary>
 ///<remarks> 
 /// Created By Alexander Tilley 12/08/2017
  ///Edited By Jeffrey Albion: Added interaction through Light Interface
 ///</remarks>
public class FloodLight : MonoBehaviour, shipLight {

	///Recomended to have both objects as children from the object this script is apart of
	///The Light On Object (Should contain Unity Light as child)
	public GameObject lightOn;	
	///The Light Off Object (should not have Emission map) 
	public GameObject lightOff;		


	/// Intitalally loaded object
	void Start () {
		if (lightOn != null && lightOff != null) {	//If Both Objects are set
				lightOn.SetActive (false);
				lightOff.SetActive (true);
				//Debug.Log("Flood Ligth Power Off");
		}
	}


	/// Switches the between the diffrent game objects
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
