using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class CountDownTimer : MonoBehaviour {
    public float Time = 50.0f;
    float closetime = 3.0f;
    float alertclosetime = 3.0f;
    bool Gameover = false;

    public GameObject tutorialwindow;

    public AudioClip IDSALERTclip;
    AudioSource IDSALERTsource;
    public AudioClip Firewallclip;
    AudioSource Firewallsource;

    HackingUI UI;

    public Text text;
    public Text alertText;

    public void reset() {
        Debug.Log("Timer reset");
        Time = 50.0f;
        closetime = 3.0f;
        alertclosetime = 3.0f;
        Gameover = false;
    }

    private void Start()
    {
        IDSALERTsource = UI.getTerminal().IDSALERTsource;
        Firewallsource = UI.getTerminal().Firewallsource;

        IDSALERTsource.clip = IDSALERTclip;
        Firewallsource.clip = Firewallclip;
        Firewallsource.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 1f);
        IDSALERTsource.volume = ((PlayerPrefs.GetFloat("SFXSound")) * 1f);
    }

    void Update() {
        if (Gameover) {
            closetime -= UnityEngine.Time.deltaTime;
        }
        else
        { 
            if(!tutorialwindow.activeInHierarchy)
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
        Time = Time - 15;
        alertText.text = "INTRUSION DETECTED!";
        IDSALERTsource.Play();
    }

    public void Firewallclicked()
    {
       Firewallsource.Play();
    }
}