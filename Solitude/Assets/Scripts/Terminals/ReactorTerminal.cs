using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorTerminal : Terminal {

    ReactorUI RecUI;

    public static ReactorTerminal reactorObj;

    public int powerUnits;
    bool online;
    bool overload;
    float fillRate = 0.01f;
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
    protected override void initialise() {
        RecUI = ui.GetComponent<ReactorUI>();
        online = true;
    }
    public void SetRod(int rod) {
        RecUI.controlRod.value = rod;
        RecUI.cRodNum.text = RecUI.controlRod.value.ToString();
    }
    public void powerUP() {
        if (RecUI.tempGage.value == 0) {
            powerUnits = PowerSystem.restore();
            online = true;
            RecUI.powerUsage.text = powerUnits.ToString();
            RecUI.status.text = "Light Load";
            RecUI.shutdown.interactable = true;
        }
    }
    public void ReactorOverload() {
        RecUI.status.text = "Cooling";
        online = false;
        overload = true;
        SetRod(100);
        powerUnits = PowerSystem.crash(); //removed when set draw is implemented
        RecUI.powerUsage.text = powerUnits.ToString();
        //code to flip breakers.
        RecUI.shutdown.interactable = false;
    }
    public void EmergencyPowerDown() {
        RecUI.status.text = "Cooling";
        online = false;
        overload = false;
        SetRod(100);
        powerUnits = PowerSystem.crash(); //removed when set draw is implemented
        RecUI.powerUsage.text = powerUnits.ToString();
        RecUI.shutdown.interactable = false;
    }
    public void setDraw(int draw) {
        powerUnits = draw;
        RecUI.powerUsage.text = draw.ToString();
    }
    // Update is called once per frame
    protected override void doUpdate() {
        if (Input.GetMouseButtonUp(0)) {
            RecUI.shutdown.onClick.AddListener(() => EmergencyPowerDown());
        }
        if (RecUI.tempGage.value == 0) {
            powerUP();
        }
        if (RecUI.tempGage.value == 100) {
            ReactorOverload();
        }
    }
    void LateUpdate() {
        RecUI.powerUsage.text = powerUnits.ToString();
        RecUI.tempNum.text = RecUI.tempGage.value.ToString();
        
        if (!online) {
            if (overload) {
                sCool();
            } else if (!overload) {
                fCool();
            }
        }
    }

    public void fCool() {
        RecUI.tempGage.value -= DecRate * fillRateMult;
    }
    public void sCool() {
        RecUI.tempGage.value -= DecRate;
    }

    public override void interact() {
        if (Ship.ship.getAccess()) {
            showUI(true);
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void onClose() {
        showUI(false);
    }
}
