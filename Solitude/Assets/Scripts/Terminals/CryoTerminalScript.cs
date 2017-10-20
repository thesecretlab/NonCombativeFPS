using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///Handles all elements associated with the cryo terminal user interface
/// <summary>
public class CryoTerminalScript : Terminal, PowerConsumer
{
	
	///Array of gameobjects handling all crew objects
    public GameObject[] crew;    
	///Game object handling the crew power
    GameObject crew_power;  
	///Game object handling the crew health	
    GameObject crew_health;   
	///text that displays power offline or online status for each crew member	
    Text crew_power_text;   
	///text that displays the health from 0 to 100 for the crew members	
    Text crew_health_text;   
	///bool to handle power on or off status	
    bool powervalue = false;   
	///bool to handle crew dead or alive status	
    bool crewdead = false;  
	///bool to handle if all crew are dead or not. 	
    public bool allcrewdead = false;  
	///variable that holds the health value of the crew member	
    float CrewHealthNum = 100.0f;    
	///counter used in counting crew members	
    int crewcount = 0;                  
	///value represents the Power required to power the cryoroom
    int needPower = 11; 

    ///Called when player interacts with the in game terminal object.
    public override void interact()
    {
        ///loads the cryo terminal user interface
        show();

        ///Searches through all gameobjects in the heirarchy of the cryoterminal scene that are named "POWER"
        ///and grabs the text component.
        foreach (GameObject c in crew) {
            crew_power = c.transform.Find("POWER").gameObject;
            crew_power_text = crew_power.GetComponent<Text>();

            ///if the cryoroom has no power set crew power gameobject text to display OFFLINE
            if (powervalue == false) {
                crew_power_text.text = ("OFFLINE");
            }

            ///if the cryoroom has power set crew power gameobject text to display ONLINE
            if (powervalue == true)
            {
                crew_power_text.text = ("ONLINE");
            }

        }
    }

    ///doUpdate updates the user interface in real time according to frames per second. 
    protected override void doUpdate()
    {
        ///Searches through all gameobjects in the heiarchy of the cryoterminal scene that are named "HEALTH"
        ///and grabs the text component.
        foreach (GameObject c in crew)
        {
            crew_health = c.transform.Find("HEALTH").gameObject;
            crew_health_text = crew_health.GetComponent<Text>();

            ///if there is now power to the cryo room and all the crew are not dead
            if (powervalue == false && allcrewdead == false)
            {
                ///if crew members health is greater than zero
                if(CrewHealthNum > 0.0f)
                {
                    ///decrease the crew members health by 0.0001f and update the crew health text to display the current value
                    CrewHealthNum = CrewHealthNum - 0.0001f;
                    crew_health_text.text = Mathf.Floor(CrewHealthNum) + "%";
                }
                else
                {
					///Text is displayed via pop up to the player HUD
                    Toast.addToast("Crew member died", 3); 
					///changes the health text of the associated crew member to deceased. 
                    crew_health_text.text = "DECEASED"; 
					///crew member has died.
                    crewdead = true; 
					///crew member has died, add to crew count variable.
                    crewcount++;  

                }
            }

            ///if crew count has reached 41
            if(crewcount == 41) 
            {
                ///call the function allDead from the gameconditions script. which will cause a gameover state to occur. 
                GameConditions.allDead();
            }
            
            ///if cryo room has power but all the crew are not dead.
            if (powervalue == true && allcrewdead == false)
            {
                ///if crew members health less than 100
                if(CrewHealthNum < 100)
                {
                    ///incrememnt crew health value by 0.00005f and update the crew health text to display the current value
                    CrewHealthNum = CrewHealthNum + 0.00005f;
                    crew_health_text.text = Mathf.Floor(CrewHealthNum) + "%";
                }
       
            }
        }
    }

    ///initialization function 
    protected override void initialise()
    {

        ///creates a list of all crew members in the cryo scene hierarchy 
        List<GameObject> cl = new List<GameObject>();
        foreach (Text go in ui.GetComponentsInChildren<Text>())
        {
			///add gameobject to list if "crew" is somewhere in its name. 
            if (go.name.Contains("Crew")) 
            {
                cl.Add(go.gameObject);
            }
        }
        crew = cl.ToArray();

    }

	///Unused function at this time
    protected override void onClose()
    {

    }

    ///the function is called when the power system changes the amount of power delivered to that object. 
    ///Inherited from the power consumer interface
    public void updatePower(bool powered) {
        String toast = powered ? "Cryo Power Restored" : "WARNING! Cryo Power Failure";
        Toast.addToast(toast, 3);
        powervalue = powered;
    }
}
