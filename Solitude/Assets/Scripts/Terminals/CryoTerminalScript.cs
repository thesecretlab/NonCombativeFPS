using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryoTerminalScript : Terminal
{
    public GameObject[] crew;
    GameObject crew_power;
    GameObject crew_health;
    Text crew_power_text;
    Text crew_health_text;
    bool powervalue = false;
    float CrewHealthNum = 100.0f;
    float decreasehealthRate = 0.01f;

    public override void interact()
    {
        showUI(true);
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

            if(powervalue == false)
            {
                CrewHealthNum = CrewHealthNum - 0.01f;
                crew_health_text.text = Mathf.Floor(CrewHealthNum) + "%";
            }   
            else if (CrewHealthNum <= 100)
            {
                CrewHealthNum += 0.05f;
               
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
        showUI(false);

    }
}
