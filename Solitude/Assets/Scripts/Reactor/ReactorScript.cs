using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorScript : MonoBehaviour {


    public int recTemp = 25; // This will able to be accessed from everywhere to get the reactor temperature
    Text powerUsage;
    int powerUnits;
    Slider controlRod;
    Slider tempGage;
    double heatInc;
    Text status;
    int online; //0 offline, 1 pending, online 2
    Button shutdown;

	// Use this for initialization
	void Start () {
        

    }

    public void increaseTemp(int powerOutput)
    {
        if(powerOutput < 100)
        {
            heatInc = heatInc + 0.5;
        }

        else if(powerOutput >= 100 && powerOutput < 200)
        {
            heatInc = heatInc + 1;
        }
        else if(powerOutput >= 200)
        {
            heatInc = heatInc + 2;
        }
        else if(online == 0)
        {
            heatInc = heatInc - 5;
        }
        else if (online == 1)
        {
            heatInc = heatInc - 2;
        }

    }
    public void powerUP()
    {
        if (heatInc <= 0)
        {
            online = 2;
            status.text = "online";
        }
    }

    public void ReactorOverload()
    {
        online = 1;
        status.text = "Offline";
        powerUsage.text = "0";
    }
    
    public void EmergencyPowerDown()
    {
        online = 0;
        status.text = "Offline";
        powerUsage.text = "0";
    }

    public void setDraw(int draw)
    {
        if (online == 2)
        {
            powerUnits = draw;
            powerUsage.text = draw.ToString();
        }

    }
    // Update is called once per frame
    void Update () {

        increaseTemp(powerUnits);
        tempGage.value = (int)heatInc;

        if (online == 2)
        {
            powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
            controlRod = GameObject.Find("cRod").GetComponent<Slider>();
            tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
            shutdown = GameObject.Find("EmerBut").GetComponent <Button>();

            

            if (tempGage.value >= 100)
            {
                ReactorOverload();
                online = 2;
            }


        }
        else if(online != 2)
        {
            powerUP();
        }
     

    }
}
