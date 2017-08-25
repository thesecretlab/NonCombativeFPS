using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodeaccess4 : MonoBehaviour
{

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
 

    int button1_on;
    int button2_on;
    int button3_on;
    int button4_on;

    // Use this for initialization
    void Start()
    {
        Button btn = Button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskButton1);

        Button btn2 = Button2.GetComponent<Button>();
        btn2.onClick.AddListener(TaskButton2);

        Button btn3 = Button3.GetComponent<Button>();
        btn3.onClick.AddListener(TaskButton3);

        Button btn4 = Button4.GetComponent<Button>();
        btn4.onClick.AddListener(TaskButton4);


        if (Button1.gameObject.name == ("START"))
        {
            button1_on = 1;
        }
    }
    void TaskButton1()
    {
        if(GlobalVars.GlobalVariables.INACCESSIBLE == 1.0f)
        {
            button1_on = 0;
        }
        else
        {
            button1_on = 1;
        }
    }
    void TaskButton2()
    {
        if (GlobalVars.GlobalVariables.INACCESSIBLE == 1.0f)
        {
            button2_on = 0;
        }
        else
        {
            button2_on = 1;
        }
    }
    void TaskButton3()
    {
        if (GlobalVars.GlobalVariables.INACCESSIBLE == 1.0f)
        {
            button3_on = 0;
        }
        else
        {
            button3_on = 1;
        }
    }
    void TaskButton4()
    {
        if (GlobalVars.GlobalVariables.INACCESSIBLE == 1.0f)
        {
            button4_on = 0;
        }
        else
        {
            button4_on = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (button1_on == 1 || button2_on == 1 || button3_on == 1 || button4_on == 1)
        {
            Button5.interactable = true;
        }
        else
        {
            Button5.interactable = false;
        }
    }
}
