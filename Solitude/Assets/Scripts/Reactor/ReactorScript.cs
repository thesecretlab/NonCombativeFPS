using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorScript : MonoBehaviour {


    int recTemp = 25; // This will able to be accessed from everywhere to get the reactor temperature
    Text powerUsage;
    int powerUnits;
    Slider controlRod;
    Slider tempGage;
    double heatInc;
    Text status;
    int online = 2; //0 offline, 1 pending, online 2
    GameObject shutdown;

	// Use this for initialization
	void Start () {

        powerUnits = 5;
    }

    public double increaseTemp(int powerOutput, int online)
    {

        if(powerOutput <= 4 && powerOutput > 0)
        {
            heatInc = heatInc + 0.5;
            controlRod.value = 75;
        }

        else if(powerOutput > 4 && powerOutput <= 8)
        {
            heatInc = heatInc + 1;
            controlRod.value = 50;
        }
        else if(powerOutput >= 9 && powerOutput < 12)
        {
            heatInc = heatInc + 2;
            controlRod.value = 25;
        }
        else if (powerOutput >= 12)
        {
            heatInc = heatInc + 4;
            controlRod.value = 0;
        }
        else if(online == 0)
        {
            heatInc = heatInc - 5;
        }
        else if (online == 1)
        {
            heatInc = heatInc - 2;
        }

        return heatInc;

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
        powerUnits = 0; //removed when set draw is implemented
        controlRod.value = 100;
    }
    
    public void EmergencyPowerDown()
    {
        online = 0;
        powerUsage.text = "0";
        controlRod.value = 100;
        powerUnits = 0; //Remove when set draw is implemented
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

        
        
        

        

        status = GameObject.Find("ReacStat").GetComponent<Text>();
        tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
        powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
        controlRod = GameObject.Find("cRod").GetComponent<Slider>();
        shutdown = GameObject.Find("EmerBut");


        



        switch (online){

            case 0:
                status.text = "offline";
                shutdown.GetComponent<Button>().interactable = false;
                break;
            case 1:
                status.text = "Cooling";
                shutdown.GetComponent<Button>().interactable = false;
                break;
            case 2:
                status.text = "Online";
                powerUnits = 5;//removed when setDraw is implemented
                shutdown.GetComponent<Button>().interactable = true;
                break;
        }


        heatInc = increaseTemp(powerUnits, online);
        tempGage.value = (int)heatInc;
        powerUsage.text = powerUnits.ToString();

        if (online == 2)
        {



            if (tempGage.value >= 100)
            {
                ReactorOverload();
                online = 1;
            }


        }
        else if(online != 2)
        {
            if (tempGage.value == 0)
            {
                powerUP();
            }
        }
     

    }
}
