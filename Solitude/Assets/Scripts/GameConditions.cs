using System.Collections;
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

    static GameConditions gamecond;

    public bool gotspeed = false;
    public bool traveling = true;

    public float shiphealth = 100;              //variable that holds ship health value
    public float TurretAccuracy = 60f;          //variable that holds turret accuracy value
    float AsteroidHitChance;                    //variable that holds asteroid hitchance
    bool AsteroidOccurance = false;             //boolean value that determines if an asteroid event occurs
    public float GameTime = 600.0f;             //Minimum game time variable
    public Text TimeLefttext;                   //Text that displays time left on bridge screen.
    public Text ShipHealthtext;                 //Text to display on bridge screen to update time
    bool GameoverCrewdead;                      //boolean value game over if crew dead
    bool GameoverShipdead;                      //boolean value game over if ship dead
    public GameObject EndGameWindow;            //Window that displays the end game screen
    public AudioClip Explosionclip;             //Audio file to be played for explosion
    public AudioSource Explosionsource;         //source of the audio for explosion

    

    public Text EndGameText;					//Text Object to display at endgame

    //function sets the accuracy of the asteroid defence system 
    //function for other objects to call to set the fields of the static object
    public static void setAccuracy(float acc) {
        gamecond.TurretAccuracy = acc;
    }

    //fucntion sets the ship to be "travelling" which means the game time counts down
    //function for other objects to call to set the fields of the static object
    public static void setTraveling(bool travel) {
        gamecond.traveling = travel;
    }

    //functions sets crew to be dead, forcing game over condition.
    //function for other objects to call to set the fields of the static object
    public static void allDead() {
        gamecond.GameoverCrewdead = true;
    }


    // on awake,sets itself as the static gamecondition, or deletes itself if another one is found
    // the static allows it to be directly referenced by other scripts
    void Awake() {
        if (gamecond == null) {
            gamecond = this;
        } else {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start ()
    {
        Debug.Log(name);
        ShipHealthtext.text = "Hull Integrity " + shiphealth + " %";    //text to be displayed on bridge screen.
        Explosionsource.clip = Explosionclip;                           //sets up audio file for asteroid hits/explosions

    }
	
	// Update is called once per frame
	void Update ()
    {
        AsteroidHitChance = (100 - TurretAccuracy);                     //sets asteroid hit chance to be that of 100 minus turret accuracy
        AsteroidChance();                                               //asteroidchance function called, (handles asteroid event occurance)

        if(AsteroidOccurance == true && shiphealth >= 0)                //if an asteroid event occurs and ship health is greater or equal to 0
        {
            float random1 = Random.Range(0, 1000);                      //Select a value at random between the given number range
            {
                if(random1 >= 0 && random1 <= AsteroidHitChance)        //if selected value is greater/equal to 0  and less than/equal to asteroidhitchance varaible
                {
                    float random2 = Random.Range(1, 7);                 //select a value at random between the given number range.
                    {
                        shiphealth = shiphealth - random2;              //ship health is equal to ship health minus the value of the random2 variable (taking damage)
                        
                        Explosionsource.Play();                         //plays the explosion audio clip attached to in game object
                        
                    }
                    
                }
            }
        }
        ShipHealthtext.text = "";
        if (TurretAccuracy < 50) {                                                      //If turretaccuracy is less than
            ShipHealthtext.text = "Defence Accuracy Low\nRecalibration Necessary\n";    //Display text on bridge screen
        }
        ShipHealthtext.text += "Hull Integrity " + shiphealth + " %";                   //Display text on bridge screen

        if (traveling) {                                                                //if navigation is working
            if (gotspeed) {                                                             //if FTL has power
                GameTime -= Time.deltaTime;                                             //Countdown GameTime value
                TimeLefttext.text = "Time Remaining:  " + Mathf.Round(GameTime);        //Display value of GameTime on the bridge screen in game. 
            } else {
                TimeLefttext.text = "Power Failure";                                    //Display only the text "Power Failure"
            }
        } else {
            TimeLefttext.text = "Navigation Error";                                     //Display only the text "Navigation Error"
        }
        

        if(GameTime < 0)										//Player Wins
        {
			EndGameWindow.SetActive(true);						//Display EndGame Window
            Player.playerObj.FPSEnable(false);
        }
        if(GameoverCrewdead)                                    //Player loses game over crew dead.
        {
            loseGame("The crew are dead");                      //Call lose game function, display text
        }

        if(shiphealth <= 0)                                      //player loses game over ship dead.
        {
            loseGame("The ship has been destroyed");            //Call lose game function, display text
        }
    }

    // functions handles wether an asteroid event occurs or not, picking a value from a random number range. 
    void AsteroidChance()
    {
        float random = Random.Range(0, 10000);      //Select random value and assign to variable between specified number range
        {
            if (random < 65 && random > 50)         //If selected random value is between specified number range
            {
                AsteroidOccurance = true;           //Asteriod event occurs (is true)
            }
            else
            {
                AsteroidOccurance = false;          //Asteroid event doesnt occur (is false)
            }
        }

    }

	//Triggers lose game state and displays addtional text if needed
	public void loseGame(string optional){
		if (optional == null) {				//If nothing was entred
			optional = "";
		}
		EndGameText.color = Color.red;                  //sets endgame color to red
		EndGameText.text = "Game Over \n" + optional;	//Sets endgame text to display game over plus optional text depending on the game over condition
		EndGameWindow.SetActive(true);                  //enables engame window
		Player.playerObj.FPSEnable(false);              //disables all in game frames

	}

    //sets speed of the ship
    //function for other objects to call to set the fields of the static object
    public static void setSpeed(bool v) {
        gamecond.gotspeed = v;
    }
}
