using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * Returns the ButtonID to the screw pannel that created it so it can be deleted.
 * 
 * By Alexander Tilley 2/10/2017
 */
public class onClickScrew : MonoBehaviour {

	private Button self; 		//Itself
	public int buttonID;		//Value to return when clicked (set by instanciator) 
	public ScrewPanel parent;	//The ScrewPannel that created it (The Insanciator)

	// Use this for initialization
	void Start () {
		self = this.GetComponent<Button>();			//Make it a button
		self.onClick.AddListener (pressed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void pressed(){
		Debug.Log ("Button Pressed Screw: " + buttonID);
		//parent.clickButton (buttonID);
	}


}
