using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathwayActivation : MonoBehaviour {

    public Button Button1;
    public Button Button2;

    int button1_on;
    int button2_on;
    public Image Pathway;
    // Use this for initialization
    public void Start() {
        Button btn = Button1.GetComponent<Button>();
        btn.onClick.AddListener(Button1active);
    
        Button btn2 = Button2.GetComponent<Button>();
        btn2.onClick.AddListener(Button2active);

        if (Button1.gameObject.name == ("START"))
        {
            button1_on = 1;
        }

    }

    void Button1active()
    {
        button1_on = 1;
    }
    void Button2active()
    {
        button2_on = 1;
    }

    public void Update()
    {

        if (button1_on == 1 && button2_on == 1)
        {
            Pathway.GetComponent<Image>().color = Color.cyan;
        }
    }
}
