using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorTerminal : Terminal {

    public bool doBreak;

    ReactorUI RecUI;
    
    public static ReactorTerminal reactorObj;

    public int powerUnits;
    bool online;
    bool overload;
    public float fillRate = 0.00001f;
    float DecRate = 0.1f;
    int fillRateMult = 2;

    private float temp;
    private float rod;
    
    // Use this for initialization
    void Awake() {
        if (reactorObj == null) {
            reactorObj = this;
        } else {
            Destroy(transform.gameObject);
        }
    }

    public float getTemp() {
        return temp < 0 ? 0 : temp;
    }
    public float getRod() {
        return rod;
    }
    public int getPow() {
        return powerUnits;
    }
    public float getTime() {
        return online ? 0 : temp;
    }
    public string getStatus() {
        if (!online || powerUnits == 0) {
            return "Cooling";
        }
        if (powerUnits <= 15) {
            return "Light Load";
        }
        if (powerUnits < 34) {
            return "Medium Load";
        }
        if (powerUnits < 49) {
            return "Heavy Load";
        }
        return "Max Load";
    }

    protected override void initialise() {
        RecUI = ui.GetComponent<ReactorUI>();
        RecUI.setTerminal(this);
        online = true;
        SetRod(100);
        temp = 0;
    }

    public void SetRod(float rod) {
        this.rod = rod;
        powerUnits = (int)(100 - rod) / 2;
        PowerSystem.setPower(powerUnits);
    }


    public void powerUP() {
        if (temp <= 0) {
            powerUnits = PowerSystem.restore();
            online = true;
            RecUI.restart.interactable = false;
            RecUI.shutdown.interactable = true;
            RecUI.controlRod.interactable = true;
        }
    }

    public void powerDown(bool crash) {
        if (crash) {
            Toast.addToast("Reactor overload\n Powering Down", 3);
            //TODO
            //code to flip breakers
        }
        overload = crash;

        online = false;
        SetRod(100);
        powerUnits = PowerSystem.crash();

        RecUI.restart.interactable = true;
        RecUI.controlRod.interactable = false;
        RecUI.shutdown.interactable = false;
    }
 
    // doUpdate is called once per frame
    protected override void doUpdate() {
        if (doBreak) {
            doBreak = false;
            temp = 100;
        }

        if (temp >= 100) {
            powerDown(true);
        }

        if (online && powerUnits != 0) {
            heatingUp();
        } else {
            coolingDown();
        }
    }

    public void heatingUp() {
        temp += (((Time.deltaTime + powerUnits / 4) *fillRate));
    }

    public void coolingDown() {
        int mod = 1;
        if (!overload) {
            mod = fillRateMult;
        }
        temp = temp <= 0 ? 0 : temp - DecRate * mod;
    }

    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void onClose() {}
}
