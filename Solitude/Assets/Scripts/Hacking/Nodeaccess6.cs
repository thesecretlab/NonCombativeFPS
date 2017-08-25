using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodeaccess6 : MonoBehaviour {

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public Button Button6;
    public Button Button7;

    int button1_on;
    int button2_on;
    int button3_on;
    int button4_on;
    int button5_on;
    int button6_on;
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

        Button btn5 = Button5.GetComponent<Button>();
        btn5.onClick.AddListener(TaskButton5);

        Button btn6 = Button6.GetComponent<Button>();
        btn6.onClick.AddListener(TaskButton6);

    
        if (Button3.gameObject.name == ("START"))
        {
            button1_on = 1;
        }
    }
    void TaskButton1()
    {
        button1_on = 1;
    }
    void TaskButton2()
    {
        button2_on = 1;
    }
    void TaskButton3()
    {
        button3_on = 1;
    }
    void TaskButton4()
    {
        button4_on = 1;
    }
    void TaskButton5()
    {
        button5_on = 1;
    }
    void TaskButton6()
    {
        button6_on = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (button1_on == 1 || button2_on == 1 || button3_on == 1 || button4_on == 1 || button5_on == 1 || button6_on == 1)
        {
            Button7.interactable = true;
        }
        else
        {
            Button7.interactable = false;
        }
    }
}
