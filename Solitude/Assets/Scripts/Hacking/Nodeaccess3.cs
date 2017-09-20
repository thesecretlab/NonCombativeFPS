using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodeaccess3 : MonoBehaviour
{

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;



    int button1_on;
    int button2_on;
    int button3_on;


    // Use this for initialization
    void Start()
    {
        Button btn = Button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskButton1);

        Button btn2 = Button2.GetComponent<Button>();
        btn2.onClick.AddListener(TaskButton2);

        Button btn3 = Button3.GetComponent<Button>();
        btn3.onClick.AddListener(TaskButton3);

        if (Button1.gameObject.name == ("START"))
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

    // Update is called once per frame
    void Update()
    {
        if (button1_on == 1 || button2_on == 1 || button3_on == 1)
        {
            Button4.interactable = true;
        }
        else
        {
            Button4.interactable = false;
        }
    }
}
