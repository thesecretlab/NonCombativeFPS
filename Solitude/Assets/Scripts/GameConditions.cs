﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * Controls and displays the win and lose condition for the game which is a timer.
 * Displays the countdown timer on a canvas
 * 
 *Created by Jonothan
 *Modified by Alexander Tilley 23/09/2017
 *
 */
public class GameConditions : MonoBehaviour {

    public float Time = 5.0f;
    public Text text;							//Text to display on bridge screen to update time
    //bool Gameover = false;					
    public GameObject EndGameWindow;			//Window that displays the end game screen

	public Text EndGameText;					//Text Object to display at endgame

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        Time -= UnityEngine.Time.deltaTime;
        text.text = "Time \nRemaining:  " + Mathf.Round(Time);

        if(Time < 0)											//Player Wins
        {
			EndGameWindow.SetActive(true);						//Display EndGame Window
            Player.playerObj.FPSEnable(false);
        }
    }

	//Triggers lose game state and displays addtional text if needed
	public void loseGame(string optional){
		if (optional == null) {				//If nothing was entred
			optional = "";
		}
		EndGameText.color = Color.red;
		EndGameText.text = "Game Over \n" + optional;	//Change Text
		EndGameWindow.SetActive(true);
		Player.playerObj.FPSEnable(false);

	}
}
