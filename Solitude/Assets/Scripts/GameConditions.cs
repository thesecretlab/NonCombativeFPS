using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

///<summary>
///Controls and displays the win and lose condition for the game which is a timer.
///Displays the countdown timer on a canvas
/// 
///Created by Jonothan Nield
///Modified by Alexander Tilley 23/09/2017
///
///<summary>
public class GameConditions : MonoBehaviour {

	///static GameConditions variable
    static GameConditions gamecond;
	///Bool variable for ship speed true or false
    public bool gotspeed = false;
	///Bool variable for ship navigations true or false
    public bool traveling = true;
	///variable that holds ship health value
    public float shiphealth = 100;  
	///variable that holds turret accuracy value	
    public float TurretAccuracy = 60f;  
	///variable that holds asteroid hitchance	
    float AsteroidHitChance;   
	///boolean value that determines if an asteroid event occurs	
    bool AsteroidOccurance = false;   
	///Minimum game time variable	
    public float GameTime = 600.0f;        
	///Text that displays time left on bridge screen.	
    public Text TimeLefttext;         
	///Text to display on bridge screen to update time	
    public Text ShipHealthtext;   
	///boolean value game over if crew dead	
    bool GameoverCrewdead;  
	///boolean value game over if ship dead	
    bool GameoverShipdead;          
	///Window that displays the end game screen	
    public GameObject EndGameWindow; 
	///Audio file to be played for explosion	
    public AudioClip Explosionclip;  
	///source of the audio for explosion	
    public AudioSource Explosionsource;         

    

	///Text Object to display at endgame
    public Text EndGameText;					

    ///function sets the accuracy of the asteroid defence system 
    ///function for other objects to call to set the fields of the static object
    public static void setAccuracy(float acc) {
        gamecond.TurretAccuracy = acc;
    }

    ///fucntion sets the ship to be "travelling" which means the game time counts down
    ///function for other objects to call to set the fields of the static object
    public static void setTraveling(bool travel) {
        gamecond.traveling = travel;
    }

    ///functions sets crew to be dead, forcing game over condition.
    ///function for other objects to call to set the fields of the static object
    public static void allDead() {
        gamecond.GameoverCrewdead = true;
    }


    /// on awake,sets itself as the static gamecondition, or deletes itself if another one is found
    /// the static allows it to be directly referenced by other scripts
    void Awake() {
        if (gamecond == null) {
            gamecond = this;
        } else {
            Destroy(gameObject);
        }
    }
    /// Use this for initialization
    void Start ()
    {	///debugging purposes
        Debug.Log(name);
		///text to be displayed on bridge screen.
        ShipHealthtext.text = "Hull Integrity " + shiphealth + " %"; 
		///sets up audio file for asteroid hits/explosions
        Explosionsource.clip = Explosionclip;                       

    }
	
	/// Update is called once per frame
	void Update ()
    {   ///sets asteroid hit chance to be that of 100 minus turret accuracy
        AsteroidHitChance = (100 - TurretAccuracy);   
		///asteroidchance function called, (handles asteroid event occurance)		
        AsteroidChance();                                             
       ///if an asteroid event occurs and ship health is greater or equal to 0
        if(AsteroidOccurance == true && shiphealth >= 0)         
        {   ///Select a value at random between the given number range
            float random1 = Random.Range(0, 1000);              
            {   ///if selected value is greater/equal to 0  and less than/equal to asteroidhitchance varaible
                if(random1 >= 0 && random1 <= AsteroidHitChance)   
                {   ///select a value at random between the given number range.
                    float random2 = Random.Range(1, 7);            
                    {   ///ship health is equal to ship health minus the value of the random2 variable (taking damage)
                        shiphealth = shiphealth - random2;          
                        ///plays the explosion audio clip attached to in game object 
                        Explosionsource.Play();                     
                        
                    }
                    
                }
            }
        }
        ShipHealthtext.text = "";
		 ///If turretaccuracy is less than 50
        if (TurretAccuracy < 50) {                
			///Display text on bridge screen		
            ShipHealthtext.text = "Defence Accuracy Low\nRecalibration Necessary\n";    
        }
		///Display text on bridge screen
        ShipHealthtext.text += "Hull Integrity " + shiphealth + " %";                   
		///if navigation is working
        if (traveling) {    
			///if FTL has power		
            if (gotspeed) { 
				///Countdown GameTime value			
                GameTime -= Time.deltaTime;     
				///Display value of GameTime on the bridge screen in game. 				
                TimeLefttext.text = "Time Remaining:  " + Mathf.Round(GameTime);        
            } else {
				///Display only the text "Power Failure"
                TimeLefttext.text = "Power Failure";                                    
            }
        } else {
			///Display only the text "Navigation Error"
            TimeLefttext.text = "Navigation Error";                                     
        }
        
		///Player Wins
        if(GameTime < 0)										
        {
			///Display EndGame Window
			EndGameWindow.SetActive(true);	
			///disables all in game frames				
            Player.playerObj.FPSEnable(false);
        }
		///Player loses game over crew dead.
        if(GameoverCrewdead)                                    
        {
			///Call lose game function, display text
            loseGame("The crew are dead");                      
        }
		///player loses game over ship dead.
        if(shiphealth <= 0)                                      
        {
			///Call lose game function, display text
            loseGame("The ship has been destroyed");            
        }
    }

    /// functions handles wether an asteroid event occurs or not, picking a value from a random number range. 
    void AsteroidChance()
    {
		///Select random value and assign to variable between specified number range
        float random = Random.Range(0, 10000);      
        {	///If selected random value is between specified number range
            if (random < 65 && random > 50)         
            {
				///Asteriod event occurs (is true)
                AsteroidOccurance = true;           
            }
            else
            {
				///Asteroid event doesnt occur (is false)
                AsteroidOccurance = false;          
            }
        }

    }

	///Triggers lose game state and displays addtional text if needed
	public void loseGame(string optional){
		///If nothing was entred
		if (optional == null) {				
			optional = "";
		}
		///sets endgame color to red
		EndGameText.color = Color.red;       
		///Sets endgame text to display game over plus optional text depending on the game over condition		
		EndGameText.text = "Game Over \n" + optional;
		///enables engame window		
		EndGameWindow.SetActive(true);     
		///disables all in game frames		
		Player.playerObj.FPSEnable(false);              

	}

    ///sets speed of the ship
    ///function for other objects to call to set the fields of the static object
    public static void setSpeed(bool v) {
        gamecond.gotspeed = v;
    }
}
