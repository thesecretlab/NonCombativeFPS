using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReactorUI : MonoBehaviour {
    public Text powerUsage;
    public Slider controlRod;
    public Slider tempGage;
    public Text status;
    public Button shutdown;
    public Text tempNum;
    public Text cRodNum;

    void Awake() {
        status = GameObject.Find("ReacStat").GetComponent<Text>();
        tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
        powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
        controlRod = GameObject.Find("cRod").GetComponent<Slider>();
        shutdown = GameObject.Find("EmergencyStopBut").GetComponent<Button>();
        tempNum = GameObject.Find("HeatValue").GetComponent<Text>();
        cRodNum = GameObject.Find("CRodValue").GetComponent<Text>();
    }

    public void SetRod(int rod) {
        controlRod.value = rod;
        cRodNum.text = controlRod.value.ToString();
    }
}
