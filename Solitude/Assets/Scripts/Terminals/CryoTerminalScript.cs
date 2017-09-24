using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryoTerminalScript : Terminal
{
    public GameObject[] crew;
    GameObject crew_power;
    Text crew_power_text;
    Text crew_health;
    bool powervalue = false;

    public override void interact()
    {
        showUI(true);
        foreach (GameObject c in crew) {
            crew_power = c.transform.Find("POWER").gameObject;
            crew_power_text = crew_power.GetComponent<Text>();

            if (powervalue == false) {
                crew_power_text.text = ("TEST");
            }
        }
    }

    protected override void doUpdate()
    {

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
