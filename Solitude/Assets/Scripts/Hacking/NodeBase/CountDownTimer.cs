using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

///<summary>
///Controls the countdown timer for the hackingminigame, the win conditions
///audio and game elements such as firewalls.
///<summary>
public class CountDownTimer : MonoBehaviour {
	
	///Variable holding the amount of game time left
    public float Time = 50.0f;
	///variable holds the amount of time until game closes on win condition
    float closetime = 3.0f;
	///variable holds the amount of time until game closes on win condition
    float alertclosetime = 3.0f;
	///boolean for setting game over true or false
    public bool Gameover = false;
	///Gameobject for the tutorial window popup
    public GameObject tutorialwindow;
	///Audio clip for the IDS alert
    public AudioClip IDSALERTclip;
	///Audio source for the ISDalert
    AudioSource IDSALERTsource;
	///Audio clip for the firewall 
    public AudioClip Firewallclip;
	///Audio source for the firewall
    AudioSource Firewallsource;
	///Initialise hacking user interface
    HackingUI UI;

	///Text object that displays time
    public Text text;
	///Text object that display alert messages
    public Text alertText;

	
	///Function resets varaibles on each loading of the game. 
    public void reset() {
        Debug.Log("Timer reset");
        Time = 50.0f;
        closetime = 3.0f;
        alertclosetime = 3.0f;
        Gameover = false;
    }
	///Function called on scene start
    private void Start()
    {
		///Game over set to false
        Gameover = false; 
		
		///Audio sources are referenced in the Maingame User interface 
        IDSALERTsource = UI.getTerminal().IDSALERTsource;
        Firewallsource = UI.getTerminal().Firewallsource;

		///Audio clips are initialized tied to their audio sources
        IDSALERTsource.clip = IDSALERTclip;
        Firewallsource.clip = Firewallclip;
		
		///Audio volumes are set to player preferences
        Firewallsource.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 1f);
        IDSALERTsource.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 1f);
    }
	
	///Update function called every frame
    void Update() {
		///If game over is set to true
        if (Gameover) {
			///Countdown closetime which is 3 seconds
            closetime -= UnityEngine.Time.deltaTime;
        }
        else
        { 
			///If the tutorial window is not active in heirarchy
            if(!tutorialwindow.activeInHierarchy)
			///Begin counting down the game timer
            Time -= UnityEngine.Time.deltaTime;
			///Display time reamining text
            text.text = "Time Remaining:" + Mathf.Round(Time);  
        }
		/// If closetime is less than 0
        if (closetime < 0) {
			///Close the user interface by setting doneTimer true
            UI.doneTimer();
        }
		/// If time is less than 0
        if (Time < 0) {
			///Fail is set to true which ends the game
            fail();
        }
    }
	///Function handles when a player has failed to win
    public void fail() {
		///Game over set to true
        Gameover = true;
		///Time is set to 1
        Time = 1.0f;
		///Text displays hack failed
        text.text = "#!HACK FAILED!#";
    }
	///Function called when palyer wins
    public void Hacked() {
		///Game over is set to true
        Gameover = true;
		///Text displays hack successful
        text.text = "HACK SUCCESSFUL";
    }
	///Function called to set the user interface
    public void setUI(HackingUI ui) {
		///User interface initialized
        UI = ui;
    }
	///Function called when a player clicks on an IDS node
    public void IDSclicked()
    {
		///Time loses 15 seconds
        Time = Time - 15;
		///Alert text displays intrusion detected
        alertText.text = "INTRUSION DETECTED!";
		///IDS audio plays
        IDSALERTsource.Play();
    }
///Function called when a player clicks on a firewall node
    public void Firewallclicked()
    {
		///Firewall Audio plays
       Firewallsource.Play();
    }
}