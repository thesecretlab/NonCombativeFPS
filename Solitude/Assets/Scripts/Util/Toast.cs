using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A spread of text to display to the player for a given ammount of time
/// Used in Toast script
/// </summary>
public struct Spread{
	/// Text to display
	public string text { get; set; }
	/// Ammount of time in seconds for the text to display
	public float seconds {get; set;}
}

/// <summary>
/// Displays popup text on the screen for player information for a length of specified time.
/// </summary>
/// <remarks> 
/// By Alexander Tilley (Last edit 13/10/2017)
/// Modified by INSERTNAME HERE for ????
/// </remarks>
public class Toast : MonoBehaviour {

	///
    static Toast toast;

	///The Maximum number of lines of text at one time.
	private const int queueLength = 3;
	///GameObject that displays the text.
	public GameObject textobj;
	///A queue of text to display in order of expirery.
	private Spread[] toaster = new Spread[queueLength];		//Queue in order of expirey

	/// <summary>
	/// Initialises Toast by setting up Spread[] to be empty.
	/// </summary>
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

	/// <summary>
	/// 
	/// </summary>
    void Awake() {
        if (toast == null) {
            toast = this;
        } else {
            Destroy(gameObject);
        }
    }

	/// <summary>
	/// 
	/// </summary>
	/// <returns><c>true</c>, if toast was added, <c>false</c> otherwise.</returns>
	/// <param name="text">Text.</param>
	/// <param name="seconds">Seconds.</param>
    public static bool addToast(string text, float seconds) {
        return Toast.toast.addText(text, seconds);
    }

	/// <summary>
	/// Update is called once per frame to reduce each Spread time and remove expired spreads.
	/// </summary>
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
		
	/// <summary>
	/// Adds Text to display for an ammount of time and displays in order of how quickly it should be removed.
	/// </summary>
	/// <returns><c>true</c>, if text was added, <c>false</c> otherwise.</returns>
	/// <param name="text">Text to display.</param>
	/// <param name="seconds">Seconds to display for.</param>
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
		
	/// <summary>
	/// Updates the text to display on the HUD
	/// </summary>
	void updateText(){
		textobj.GetComponent<Text> ().text = "";
		for (int i = 0; i < queueLength; i++) {								//Update Text
			textobj.GetComponent<Text> ().text += toaster[i].text+"\n";	
		}
	}
}
