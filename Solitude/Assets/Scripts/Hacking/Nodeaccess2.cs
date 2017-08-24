using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodeaccess2 : MonoBehaviour
{

    public Button Button1;
    public Button Button2;
    public Button Button3;

    int button1_on;
    int button2_on;

    // Use this for initialization
    void Start()
    {
        Button btn = Button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskButton1);

        Button btn2 = Button2.GetComponent<Button>();
        btn2.onClick.AddListener(TaskButton2);

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


    // Update is called once per frame
    void Update()
    {
        if (button1_on == 1 || button2_on == 1)
        {
            Button3.interactable = true;
        }
        else
        {
            Button3.interactable = false;
        }
    }
}
