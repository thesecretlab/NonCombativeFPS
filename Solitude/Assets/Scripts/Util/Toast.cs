using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Spread{
	public string text { get; set; }			//Text
	public float seconds {get; set;}			//Time to Display In seconds
}

/*
 * Displays Text in lower centre of screen for player information
 * 
 * Created By Alexander Tilley (Last Edit: 4/10/2017)
 */
public class Toast : MonoBehaviour {

	private const int queueLength = 3;						//Ammount of Text to display at one time
	public GameObject textobj;								//Text Object that displays Text
	private Spread[] toaster = new Spread[queueLength];		//Queue in order of expirey

	// Use this for initialization
	void Start () {
		//textobj.SetActive (false);
		textobj = gameObject;						//Get Object its attached to
		if (textobj == null) {
			Debug.Log("textobj not assigned ERROR");
		}
	}

	// Update is called once per frame
	void Update () {
		if (toaster[0].text != null && toaster [0].seconds <= 0.0f) {										//If Item to clear
			for (int i = 0; i - 1 < queueLength; i++) {								//shift queue
				toaster [i] = toaster [i + 1];
			}
			toaster [queueLength - 1].seconds = -1.0f;
			toaster [queueLength - 1].text = null;
			textobj.GetComponent<Text> ().text = "";	
			for (int i = 0; i - 1 < queueLength; i++) {								//Update Text
				textobj.GetComponent<Text> ().text = textobj.GetComponent<Text> ().text+toaster[i]+"\n";	
			}
		}
		for (int i = 0; i < queueLength; i++) {									//Remove time
			toaster [i].seconds = toaster [i].seconds - Time.deltaTime;
		}

	}

	//Adds Text to display for an ammount of time and displays in order of how quickly it should be removed
	public bool addText(string text, float seconds){
		Spread jam;
		jam.text = text;
		jam.seconds = seconds;
		for (int i = 0; i < queueLength; i++) {
			if (toaster[i].seconds >= jam.seconds || toaster[i].seconds == -1.0f) {		//If Correct Spot Insert or Empty Slot Insert
				if (toaster [i].seconds <= -1.0f) {										//IF Empty Slot Insert
					toaster [i] = jam;
					Debug.Log("Toaster placing Toast");
					return true;
				} else if((i+1)<queueLength) {											//IF toaster isnt full
					if(toaster[i+1].seconds <= -1.0f){									//If next spot is free.
						Debug.Log("Toaster reordering");
						toaster[i+1] = toaster[i];										//Reorder Toast
						toaster [i] = jam;
						return true;
					}
				}else{
					Debug.Log("Toaster FULL");												//Toaster is full.
					return false;
				}
			}
		}
		Debug.Log("Toaster ERROR");
		return false;
	}
}
