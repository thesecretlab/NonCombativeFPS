using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 
/// \brief The class containing all the functions for the reactor UI
/// 
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
    /// 
    /// \brief Used for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Gets and sets all UI elements
    /// 
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
    /// 
    /// \brief Sets the reactor terminal
    /// 
    /// \param [in] rt The reactor terminal
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void setTerminal(ReactorTerminal rt) {
        recTerm = rt;
    }
    /// 
    /// \brief Second initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Adds click listeners to UI elements
    /// 
    void Start() {
        shutdown.onClick.AddListener(delegate { EmergencyPowerDown(); });
        controlRod.onValueChanged.AddListener(delegate { SetRod(controlRod.value); });
        restart.onClick.AddListener(delegate { powerUP(); });
    }
    /// 
    /// \brief Calls the emergency power down of the reactor terminal
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void EmergencyPowerDown() {
        recTerm.powerDown(false);
    }
    /// 
    /// \brief Calls the setRod of the reactor terminal
    /// 
    /// \param [in] rod The new rod value
    /// \return No return value
    /// 
    /// \details 
    /// 
    void SetRod(float rod) {
        recTerm.SetRod(rod);
    }
    /// 
    /// \brief Calls the powerUp of the reactor terminal
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void powerUP() {
        recTerm.powerUP();
    }
    /// 
    /// \brief Called once per frame
    /// 
    /// \return No return value
    /// 
    /// \details Updates the UI elements
    /// 
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
