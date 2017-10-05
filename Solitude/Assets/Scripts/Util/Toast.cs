using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Spread{
	public string text { get; set; }			//Text
	public float seconds {get; set;}			//Time to Display In seconds
}

/*
 * Displays Text in lower centre of screen for player information for a given ammount of time
 * 
 * Created By Alexander Tilley (Last Edit: 5/10/2017)
 */
public class Toast : MonoBehaviour {

	private const int queueLength = 3;						//Ammount of Text to display at one time
	public GameObject textobj;								//Text Object that displays Text
	private Spread[] toaster = new Spread[queueLength];		//Queue in order of expirey

	// Use this for initialization
	void Start () {
		//textobj.SetActive (false);
		//textobj = gameObject;						//Get Object its attached to
		if (textobj == null) {
			Debug.Log("textobj not assigned assigning gameobject attached too");
			textobj = gameObject;						//Get Object its attached to
		}
		for (int i = 0; i < queueLength; i++) {								//Intitalise spots to empty
			toaster[i].text = "";
			toaster [i].seconds = -1.0f;
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < queueLength; i++) {									//Remove time
			if (toaster[i].text != null && toaster [i].seconds <= 0.0f) {					//If Item needs to be cleared
				for (int n = 0; (n + 1) < queueLength; n++) {								//shift queue
					toaster [n] = toaster [n + 1];
				}
				toaster [queueLength - 1].seconds = -1.0f;
				toaster [queueLength - 1].text = null;
				textobj.GetComponent<Text> ().text = "";	
				updateText ();
			}

			toaster [i].seconds = toaster [i].seconds - Time.deltaTime;			//reduce time
		}

	}

	//Adds Text to display for an ammount of time and displays in order of how quickly it should be removed
	public bool addText(string text, float seconds){
		Spread jam;
		jam.text = text;
		jam.seconds = seconds;
		for (int i = 0; i < queueLength; i++) {
			//Debug.Log ("" + toaster [i].seconds);
			if (toaster[i].seconds <= jam.seconds || toaster[i].seconds <= 0.0f) {		//If Correct Spot Insert or Empty Slot Insert
				if (toaster [i].seconds <= 0.0f) {										//IF Empty Slot Insert
					toaster [i] = jam;
					updateText ();
					//Debug.Log("Toaster: "+text+" for "+seconds+" Seconds");
					return true;
				} else if((i+1)<queueLength) {											//IF toaster isnt full
					if(toaster[i+1].seconds <= 0.0f){									//If next spot is free.
						//Debug.Log("Toaster reordering");
						toaster[i+1] = toaster[i];										//Reorder Toast
						toaster [i] = jam;
						updateText ();
						return true;
					}
				}else{
					//Debug.Log("Slot FULL");												//Toaster is full.
					//return false;
				}
			}
			//Debug.Log("Slot Full2");
			//return false;
		}
		Debug.Log("Toaster IS FULL");
		return false;
	}

	//Updates the text to display on the HUD
	void updateText(){
		textobj.GetComponent<Text> ().text = "";
		for (int i = 0; i < queueLength; i++) {								//Update Text
			textobj.GetComponent<Text> ().text += toaster[i].text+"\n";	
		}
	}
}
