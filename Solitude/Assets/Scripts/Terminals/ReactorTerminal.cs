using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorTerminal : Terminal {

    public bool doBreak;

    ReactorUI RecUI;
    
    public static ReactorTerminal reactorObj;

    public int powerUnits;
    public int powerUnitsAvail;
    bool online;
    bool overload;
    public float fillRate = 0.00001f;
    float DecRate = 0.1f;
    int fillRateMult = 2;
    
    // Use this for initialization
    void Awake() {
        if (reactorObj == null) {
            reactorObj = this;
        } else {
            Destroy(transform.gameObject);
        }
        
    }
    public void ValueChangeCheck()
    {
        PowerSystem.setPower(powerUnitsAvail);
        RecUI.cRodNum.text = RecUI.controlRod.value.ToString();
        powerUnitsAvail = powerUnits = (int)(100 - RecUI.controlRod.value) / 2;
        RecUI.powerUsage.text = powerUnitsAvail.ToString();
    }



    protected override void initialise() {
        RecUI = ui.GetComponent<ReactorUI>();
        online = false;
        SetRod(100);

        
    }
    public void SetRod(int rod) {
        RecUI.controlRod.value = rod;
        RecUI.cRodNum.text = RecUI.controlRod.value.ToString();
    }
    public void powerUP() {
        if (RecUI.tempGage.value == 0) {
            powerUnits = PowerSystem.restore();
            online = true;
            RecUI.powerUsage.text = powerUnitsAvail.ToString();
            RecUI.status.text = "Light Load";
            RecUI.shutdown.interactable = true;

        }
    }
    public void ReactorOverload() {
        Toast.addToast("Reactor overload\n Powering Down", 3);
        RecUI.status.text = "Cooling";
        online = false;
        overload = true;
        SetRod(100);
        powerUnits = PowerSystem.crash(); //removed when set draw is implemented
        RecUI.powerUsage.text = powerUnitsAvail.ToString();
        //code to flip breakers.
        RecUI.controlRod.interactable = false;
        RecUI.shutdown.interactable = false;
    }
    public void EmergencyPowerDown() {
        RecUI.status.text = "Cooling";
        online = false;
        overload = false;
        RecUI.controlRod.interactable = false;
        SetRod(100);
        powerUnits = PowerSystem.crash(); //removed when set draw is implemented
        RecUI.powerUsage.text = powerUnitsAvail.ToString();
        RecUI.shutdown.interactable = false;
    }
 

    // Update is called once per frame
    protected override void doUpdate() {
        if (doBreak) {
            doBreak = false;
            ReactorOverload();
        }
        if (Input.GetMouseButtonUp(0)) {
            RecUI.shutdown.onClick.AddListener(() => EmergencyPowerDown());
        }

        RecUI.controlRod.interactable = true;
        RecUI.controlRod.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


        if (Input.GetMouseButtonUp(0)) {
            if (!online && RecUI.tempGage.value == 0)
            {
                RecUI.restart.onClick.AddListener(() => powerUP());
            }
        }
        if (RecUI.tempGage.value == 100) {
            ReactorOverload();
        }
    }
    void LateUpdate() {
        if (online)
        {
            RecUI.controlRod.interactable = true;
            RecUI.controlRod.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            RecUI.tempNum.text = RecUI.tempGage.value.ToString();
            RecUI.restart.interactable = false;
        }
        if(!online)
        {
            RecUI.controlRod.interactable = true;
            RecUI.restart.interactable = true;
        }

        if (online) {
            if (powerUnits <= 15) {
                RecUI.status.text = "Light Load";
                heatingUp();
                //SetRod(75);
            }
            if (powerUnits >= 16 && powerUnits < 34) {
                RecUI.status.text = "Medium Load";
                heatingUp();
                //SetRod(50);
            }
            if (powerUnits >= 35 && powerUnits < 49) {
                RecUI.status.text = "Heavy Load";
                heatingUp();
                //SetRod(25);
            }
            if (powerUnits == 50) {
                RecUI.status.text = "Max Load";
                heatingUp();
                //SetRod(100);
            }
        }

        if (!online){
            if (overload) {
                sCool();
            } else if (!overload) {
                fCool();
            }
        }
    }

 

    public void heatingUp() {
        RecUI.tempGage.value += (((Time.deltaTime + powerUnitsAvail / 4) *fillRate));
    }

    public void fCool() {
        RecUI.tempGage.value -= DecRate * fillRateMult;
        RecUI.tempNum.text = RecUI.tempGage.value.ToString();
        RecUI.rTimer.text = RecUI.tempGage.value.ToString();
        fTimer(RecUI.tempGage.value);
    }
    public void sCool() {
        RecUI.tempGage.value -= DecRate;
        RecUI.tempNum.text = RecUI.tempGage.value.ToString();
        sTimer(RecUI.tempGage.value);
    }

    void sTimer(float time)
    {
        
        
            RecUI.rTimer.text = time.ToString();
            
        
    }
    void fTimer(float time)
    {
       
            RecUI.rTimer.text = time.ToString();
            
        
    }

    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void onClose() {

    }
}
