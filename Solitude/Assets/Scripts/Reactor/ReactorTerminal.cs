using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorTerminal : Terminal {

    ReactorUI RecUI;

    public static ReactorTerminal reactorObj;

    public int powerUnits;
    bool online;
    bool overload;
    float fillRate = 0.1f;
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
            powerUnits = PowerSystem.powersystem.restore();
            lowDraw();
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
        powerUnits = PowerSystem.powersystem.crash(); //removed when set draw is implemented
        RecUI.powerUsage.text = powerUnits.ToString();
        //code to flip breakers.
        RecUI.shutdown.interactable = false;
    }
    public void EmergencyPowerDown() {
        RecUI.status.text = "Cooling";
        online = false;
        overload = false;
        SetRod(100);
        powerUnits = PowerSystem.powersystem.crash(); //removed when set draw is implemented
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
        if (online) {
            if (powerUnits <= 3) {
                RecUI.status.text = "Light Load";
                lowDraw();
                SetRod(75);
            }
            if (powerUnits > 3 && powerUnits < 7) {
                RecUI.status.text = "Medium Load";
                medDraw();
                SetRod(50);
            }
            if (powerUnits > 7 && powerUnits < 10) {
                RecUI.status.text = "Heavy Load";
                hiDraw();
                SetRod(25);
            }
            if (powerUnits >= 10) {
                RecUI.status.text = "Max Load";
                maxDraw();
                SetRod(100);
            }
        }
        if (!online) {
            if (overload) {
                sCool();
            } else if (!overload) {
                fCool();
            }
        }
    }
    public void lowDraw() {
        RecUI.tempGage.value += fillRate;
    }
    public void medDraw() {
        RecUI.tempGage.value += fillRate * fillRateMult;
    }
    public void hiDraw() {
        RecUI.tempGage.value += fillRate * (fillRateMult + fillRateMult);
    }
    public void maxDraw() {
        RecUI.tempGage.value += fillRate * (fillRateMult + fillRateMult + fillRateMult);
    }
    public void fCool() {
        RecUI.tempGage.value -= fillRate * fillRateMult;
    }
    public void sCool() {
        RecUI.tempGage.value -= fillRate;
    }

    public override void interact() {
        showUI(true);
    }

    protected override void onClose() {
        showUI(true);
    }
}
