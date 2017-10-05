using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryoTerminalScript : Terminal, PowerConsumer
{
    public GameObject[] crew;
    GameObject crew_power;
    GameObject crew_health;
    Text crew_power_text;
    Text crew_health_text;
    bool powervalue = false;
    bool crewdead = false;
    public bool allcrewdead = false;
    float CrewHealthNum = 100.0f;
    int crewcount = 0;

    int needPower = 11;


    public override void interact()
    {
        show();
        foreach (GameObject c in crew) {
            crew_power = c.transform.Find("POWER").gameObject;
            crew_power_text = crew_power.GetComponent<Text>();

            if (powervalue == false) {
                crew_power_text.text = ("OFFLINE");
            }

            if (powervalue == true)
            {
                crew_power_text.text = ("ONLINE");
            }

        }
    }

    protected override void doUpdate()
    {
        foreach (GameObject c in crew)
        {
            crew_health = c.transform.Find("HEALTH").gameObject;
            crew_health_text = crew_health.GetComponent<Text>();

            if (powervalue == false && allcrewdead == false)
            {
                if(CrewHealthNum > 0.0f)
                {
                    CrewHealthNum = CrewHealthNum - 0.0001f;
                    crew_health_text.text = Mathf.Floor(CrewHealthNum) + "%";
                }
                else
                {
                    Toast.addToast("Crew member died", 3);
                    crew_health_text.text = "DECEASED";
                    crewdead = true;
                    crewcount++;

                }
            }

            if(crewcount == 41) 
            {
                GameConditions.allDead();
            }
            

            if (powervalue == true && allcrewdead == false)
            {
                if(CrewHealthNum < 100)
                {
                    CrewHealthNum = CrewHealthNum + 0.00005f;
                    crew_health_text.text = Mathf.Floor(CrewHealthNum) + "%";
                }
       
            }
        }
    }

    protected override void initialise()
    {


        List<GameObject> cl = new List<GameObject>();
        foreach (Text go in ui.GetComponentsInChildren<Text>())
        {
            if (go.name.Contains("Crew"))
            {
                cl.Add(go.gameObject);
            }
        }
        crew = cl.ToArray();

    }

    protected override void onClose()
    {

    }

    public void updatePower(bool powered) {
        String toast = powered ? "Cryo Power Restored" : "WARNING! Cryo Power Failure";
        Toast.addToast(toast, 3);
        powervalue = powered;
    }
}
