using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewiringScript : MonoBehaviour {

    //Variables to track the current state of the buttons 0 is active but unresolved, 1 means pending and 2 means resolved.
    short blue1 = 0;
    short blue2 = 0;
    short red1 = 0;
    short red2 = 0;
    short green1 = 0;
    short green2 = 0;
    string lastColourPressed;
    string colourPressed;
    GameObject blueBtn1;
    GameObject blueBtn2;
    GameObject redBtn1;
    GameObject redBtn2;
    GameObject greenBtn1;
    GameObject greenBtn2;

    // Use this for initialization
    void Start () {
        lastColourPressed = "null";
        colourPressed = "null";
        InvokeRepeating("resetWires", 3.0f, 3.0f);

    }

    


    // Update is called once per frame
    void Update () {

        

        if(blue1 == 1 && blue2 == 1)
        {
            //Disable blue1 and blue2 buttons
            GameObject blueBtn1 = GameObject.Find("BlueButton1");
            GameObject blueBtn2 = GameObject.Find("BlueButton2");

            blueBtn1.GetComponent<Button>().interactable = false;
            blueBtn2.GetComponent<Button>().interactable = false;
            blue1 = 2;
            blue2 = 2;

            resetWires();




        }

        if(red1 == 1 && red2 == 1)
        {
            //Disable red1 and red2 buttons
            GameObject redBtn1 = GameObject.Find("RedButton1");
            GameObject redBtn2 = GameObject.Find("RedButton2");

            redBtn1.GetComponent<Button>().interactable = false;
            redBtn2.GetComponent<Button>().interactable = false;

            red1 = 2;
            red2 = 2;

            resetWires();
        }

        if(green1 == 1 && green2 == 1)
        {
            //Disable green1 and green2
            GameObject greenBtn1 = GameObject.Find("GreenButton1");
            GameObject greenBtn2 = GameObject.Find("GreenButton2");

            greenBtn1.GetComponent<Button>().interactable = false;
            greenBtn2.GetComponent<Button>().interactable = false;



            green1 = 2;
            green2 = 2;

            resetWires();
        }
        //If statements to run when a incorrect sequence is added, the first 1 to check if the colours dont match and the second to make sure
        //that both strings have been assigned a colour, meaning a second button has been pressed.
     if(lastColourPressed != colourPressed)
        {
            if (colourPressed != "null")
            {
                if (red1 == 1)
                {
                    red1 = 0;
                }
                if (red2 == 1)
                {
                    red2 = 0;
                }
                if (blue1 == 1)
                {
                    blue1 = 0;
                }
                if (blue2 == 1)
                {
                    blue2 = 0;
                }
                if (green1 == 1)
                {
                    green1 = 0;
                }
                if (green2 == 1)
                {
                    green2 = 0;
                }
            }
        }

     if(blue1 + blue2 + red1 + red2 + green1 + green2 == 18)
        {
            //end the minigame with success
        }
     

	}

    public void resetWires()
    {
        if (red1 + red2 != 4)
        {
            red1 = 0;
            red2 = 0;
        }
        if (green1 + green2 != 4)
        {
            green1 = 0;
            green2 = 0;
        }
        if (blue1 + blue2 != 4)
        {
            blue1 = 0;
            blue2 = 0;
        }
    }
    

    public void redbutton1()
    {
        red1 = 1;
    }
    public void redbutton2()
    {
        red2 = 1;
    }
    public void bluebutton1()
    {
        blue1 = 1;
    }
    public void bluebutton2()
    {
        blue2 = 1;
    }
    public void greenbutton1()
    {
        green1 = 1;
    }
    public void greenbutton2()
    {
        green2 = 1;
    }
}
