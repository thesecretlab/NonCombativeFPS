using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CountDownTimer : MonoBehaviour {
    public float Time = 50.0f;
    float closetime = 3.0f;
    bool Gameover = false;

    HackingUI UI;

    public Text text;

    void Update() {
        if (Gameover) {
            closetime -= UnityEngine.Time.deltaTime;
        } else { 
            Time -= UnityEngine.Time.deltaTime;
            text.text = "Time Remaining:" + Mathf.Round(Time);
        }

        if (closetime < 0) {
            UI.doneTimer();
        }

        if (Time < 0) {
            fail();
        }
    }
    public void fail() {
        Gameover = true;
        Time = 1.0f;
        text.text = "#!HACK FAILED!#";
    }
    public void Hacked() {
        Gameover = true;
        text.text = "HACK SUCCESSFUL";
    }

    public void setUI(HackingUI ui) {
        UI = ui;
    }

    public void IDSclicked()
    {
        Time = Time - 10;
    }
}