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
    float heatInc;
    Text status;
    bool online;
    bool cooling;
    bool offline;
    GameObject shutdown;
    Text tempNum;
    Text cRodNum;

	// Use this for initialization
	void Start () {

        online = false;
        cooling = false;
        offline = false;
        heatInc = 0;
        status = GameObject.Find("ReacStat").GetComponent<Text>();
        tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
        powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
        controlRod = GameObject.Find("cRod").GetComponent<Slider>();
        shutdown = GameObject.Find("EmerBut");
        tempNum = GameObject.Find("tempNum").GetComponent<Text>();
        cRodNum = GameObject.Find("cRodNum").GetComponent<Text>();
    }

    public void increaseTemp(int powerOutput)
    {
        
        if (online)
        {
            if (powerOutput <= 4 && powerOutput > 0)
            {
                heatInc = heatInc + (float)0.05;
                controlRod.value = 75;
            }

            else if (powerOutput > 4 && powerOutput <= 8)
            {
                heatInc = heatInc + (float)0.1;
                controlRod.value = 50;
            }
            else if (powerOutput >= 9 && powerOutput < 12)
            {
                heatInc = heatInc + (float)0.2;
                controlRod.value = 25;
            }
            else if (powerOutput >= 12)
            {
                heatInc = heatInc + (float)0.4;
                controlRod.value = 0;
            }
        }

      

    }

    public void medDraw()
    {
        powerUnits = 5;
        powerUsage.text = powerUnits.ToString();
        controlRod.value = 50;
        cRodNum.text = controlRod.value.ToString();
    }
    public void liteDraw()
    {
        powerUnits = 2;
        powerUsage.text = powerUnits.ToString();
        controlRod.value = 75;
        cRodNum.text = controlRod.value.ToString();
    }

    public void heavyDraw()
    {
        powerUnits = 10;
        powerUsage.text = powerUnits.ToString();
        controlRod.value = 25;
        cRodNum.text = controlRod.value.ToString();
    }

    public void powerUP()
    {
        if (tempGage.value == 0)
        {
            online = true;
            cooling = false;
            offline = false;
            status.text = "online";
            liteDraw();
            powerUsage.text = powerUnits.ToString();
            controlRod.value = 75;
            cRodNum.text = controlRod.value.ToString();
            shutdown.GetComponent<Button>().interactable = true;
        }
    }

    public void ReactorOverload()
    {
        online = false;
        cooling = true;
        offline = false;
        powerUnits = 0; //removed when set draw is implemented
        controlRod.value = 100;
        cRodNum.text = controlRod.value.ToString();
        powerUsage.text = powerUnits.ToString();
        status.text = "Cooling";
        shutdown.GetComponent<Button>().interactable = false;
    }
    
    public void EmergencyPowerDown()
    {

        online = false;
        cooling = false;
        offline = true;

        powerUsage.text = "0";
        controlRod.value = 100;
        cRodNum.text = controlRod.value.ToString();
        powerUnits = 0; //Remove when set draw is implemented
        powerUsage.text = powerUnits.ToString();
        status.text = "Offline";
        shutdown.GetComponent<Button>().interactable = false;


        Debug.Log("button value");
        Debug.Log(online);


    }

    public void setDraw(int draw)
    {
        if (online)
        {
            powerUnits = draw;
            powerUsage.text = draw.ToString();
           
        }

    }

  
    // Update is called once per frame
    void Update () {


        /*
                if (online)
                {
                    Debug.Log("online");
                    Debug.Log(online);
                }
                if (cooling)
                {
                    Debug.Log("cooling");
                    Debug.Log(cooling);
                }
                if (offline)
                {
                    Debug.Log("offline");
                    Debug.Log(offline);
                }
                */

        tempNum.text = heatInc.ToString();
        



        if (online)
        {
            Debug.Log("increasing heat");
            increaseTemp(powerUnits);
            tempGage.value = (float)heatInc;
            tempNum.text = heatInc.ToString();
            powerUsage.text = powerUnits.ToString();
        }
        if(!online)
        {
            if (cooling)
            {
                Debug.Log("decreasing heat");
                heatInc = heatInc - (float)0.2;
                tempGage.value = (float)heatInc;
                tempNum.text = heatInc.ToString();
                powerUsage.text = powerUnits.ToString();
                Debug.Log("cooling");
            }
            else if (offline)
            {
                Debug.Log("decreasing heat");
                heatInc = heatInc - (float)0.5;
                tempNum.text = heatInc.ToString();
                tempGage.value = (float)heatInc;
                powerUsage.text = powerUnits.ToString();
                Debug.Log("fast cooling");

            }
        }



        if (online)
        {



            if (tempGage.value >= 100)
            {
                ReactorOverload();
            }


        }
        else if(!online)
        {
            if (tempGage.value <= 0)
            {
                powerUP();
                Debug.Log("powering up");
            }
        }

        tempNum.text = heatInc.ToString();



    }
}
