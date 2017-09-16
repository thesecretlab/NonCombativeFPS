using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorScript : MonoBehaviour {


    Text powerUsage;
    int powerUnits;
    Slider controlRod;
    Slider tempGage;
    Text status;
    Button ldraw;
    Button mdraw;
    Button hdraw;



    Button shutdown;
    Text tempNum;
    Text cRodNum;
    bool online;
    bool overload;
    float fillRate = 0.1f;
    int fillRateMult = 2;


    // Use this for initialization
    void Awake() {

       
        status = GameObject.Find("ReacStat").GetComponent<Text>();
        tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
        powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
        controlRod = GameObject.Find("cRod").GetComponent<Slider>();
        shutdown = GameObject.Find("EmerBut").GetComponent<Button>();
        ldraw = GameObject.Find("LowLoad").GetComponent<Button>();
        mdraw = GameObject.Find("MedLoad").GetComponent<Button>();
        hdraw = GameObject.Find("HighLoad").GetComponent<Button>();
        tempNum = GameObject.Find("tempNum").GetComponent<Text>();
        cRodNum = GameObject.Find("cRodNum").GetComponent<Text>();
        powerUP();
    }


    public void SetRod(int rod)
    {
        controlRod.value = rod;
        cRodNum.text = controlRod.value.ToString();
    }
  


    public void powerUP()
    {

        if (tempGage.value == 0)
        {

            powerUnits = 2;
            lowDraw();
            online = true;
            powerUsage.text = powerUnits.ToString();
            status.text = "Light Load";
            shutdown.GetComponent<Button>().interactable = true;
        }
        
    }


   


    public void ReactorOverload()
    {
        status.text = "Cooling";
        online = false;
        overload = true;
        SetRod(100);
        powerUnits = 0; //removed when set draw is implemented
        powerUsage.text = powerUnits.ToString();
        //code to flip breakers.
        shutdown.GetComponent<Button>().interactable = false;
    }
    
    public void EmergencyPowerDown()
    {

       
        status.text = "Cooling";
        online = false;
        overload = false;
        SetRod(100);
        powerUnits = 0; //Remove when set draw is implemented
        powerUsage.text = powerUnits.ToString();
        shutdown.GetComponent<Button>().interactable = false;

        
    }

    public void setDraw(int draw)
    {
       
        
            powerUnits = draw;
            powerUsage.text = draw.ToString();
           
        

    }

    void drawLow()
    {
        powerUnits = 2; //Remove when set draw is implemented
        powerUsage.text = powerUnits.ToString();

    }
    void drawMed()
    {
        powerUnits = 5; //Remove when set draw is implemented
        powerUsage.text = powerUnits.ToString();
    }
    void drawHigh()
    {
        powerUnits = 8; //Remove when set draw is implemented
        powerUsage.text = powerUnits.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            shutdown.onClick.AddListener(() => EmergencyPowerDown());
            ldraw.onClick.AddListener(() => drawLow());
            mdraw.onClick.AddListener(() => drawMed());
            hdraw.onClick.AddListener(() => drawHigh());
        }

        if (tempGage.value == 0)
        {
            powerUP();
        }
        if (tempGage.value == 100)
        {
            ReactorOverload();
        }


    }


       
    void LateUpdate()
    {


        powerUsage.text = powerUnits.ToString();
        tempNum.text = tempGage.value.ToString();
        if (online)
        {
            if (powerUnits <= 3)
            {
                status.text = "Light Load";
                lowDraw();
                SetRod(75);

            }

            if (powerUnits > 3 && powerUnits < 7)
            {
                status.text = "Medium Load";
                medDraw();
                SetRod(50);
            }

            if (powerUnits > 7 && powerUnits < 10)
            {
                status.text = "Heavy Load";
                hiDraw();
                SetRod(25);
            }

            if (powerUnits >= 10)
            {
                status.text = "Max Load";
                maxDraw();
                SetRod(100);
            }

        }
        if(!online)
        {
            if(overload)
            {
                sCool();
            }
            else if(!overload)
            {
                fCool();

            }

        }

  

        
    }


    public void lowDraw()
    {
        

        tempGage.value += fillRate;
    }

    public void medDraw()
    {
       

        tempGage.value += fillRate * fillRateMult;
    }

    public void hiDraw()
    {
        
        tempGage.value += fillRate * (fillRateMult + fillRateMult);
    }
    public void maxDraw()
    {
        
        tempGage.value += fillRate * (fillRateMult + fillRateMult + fillRateMult);
    }

    public void fCool()
    {
      

        tempGage.value -= fillRate * fillRateMult;
    }
    public void sCool()
    {
     
        tempGage.value -= fillRate;
    }

}
