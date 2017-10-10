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
    public Button restart;
    public Text tempNum;
    public Text cRodNum;
    public ReactorTerminal RT;
    public Text rTimer;

    private ReactorTerminal recTerm;

    void Awake() {
        status = GameObject.Find("ReacStat").GetComponent<Text>();
        tempGage = GameObject.Find("tempGage").GetComponent<Slider>();
        powerUsage = GameObject.Find("PowerUnitVal").GetComponent<Text>();
        controlRod = GameObject.Find("cRod").GetComponent<Slider>();
        shutdown = GameObject.Find("EmergencyStopBut").GetComponent<Button>();
        tempNum = GameObject.Find("HeatValue").GetComponent<Text>();
        cRodNum = GameObject.Find("CRodValue").GetComponent<Text>();
        restart = GameObject.Find("ReactorRestartBut").GetComponent<Button>();
        rTimer = GameObject.Find("RebootTimeUnityVal").GetComponent<Text>();
    }

    public void setTerminal(ReactorTerminal rt) {
        recTerm = rt;
    }

    void Start() {
        shutdown.onClick.AddListener(delegate { EmergencyPowerDown(); });
        controlRod.onValueChanged.AddListener(delegate { SetRod(controlRod.value); });
        restart.onClick.AddListener(delegate { powerUP(); });
    }

    void EmergencyPowerDown() {
        recTerm.powerDown(false);
    }

    void SetRod(float rod) {
        recTerm.SetRod(rod);
    }

    void powerUP() {
        recTerm.powerUP();
    }

    void Update() {
        controlRod.value = recTerm.getRod();
        tempNum.text = recTerm.getTemp().ToString();
        tempGage.value = recTerm.getTemp();
        cRodNum.text = recTerm.getRod().ToString();
        powerUsage.text = recTerm.getPow().ToString();
        rTimer.text = recTerm.getTime().ToString();
        status.text = recTerm.getStatus();
    }
}
