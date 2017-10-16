using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorTerminal : Terminal {
   
    
    
	///Powerlines object declaration
	public PowerLines PowerLines;

    
	/// Reactor UI object declaration.
	ReactorUI RecUI;
	///Reactor Terminal object declaration.
    public static ReactorTerminal reactorObj;

	///Variable Declarations
    public int powerUnits;
    bool online;
    bool overload;
    public float fillRate = 0.00001f;
    float DecRate = 0.1f;
    int fillRateMult = 2;
	public bool doBreak;
    private float temp;
    private float rod;
    
    /// Use this for initialization when the script is first accessed
    void Awake() {
        if (reactorObj == null) {
            reactorObj = this;
        } else {
            Destroy(transform.gameObject);
        }
    }

	///Getters for all reactor functions.
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
	/// This function monitors and updates the reactor status as things change through out the play.
    public string getStatus() {
        if (!online) {
            if (getTime() != 0) {
                if (overload) {
                    return "Overloaded";
                } else {
                    return "Flushing";
                }
            }
            if (!PowerLines.getFixed()) {
                return "Power Lines Disconnected";
            }
            return "Ready";
        }
        if (powerUnits == 0) {
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

	///Use startup initialization of UI elements.
    protected override void initialise() {
        RecUI = ui.GetComponent<ReactorUI>();
        RecUI.setTerminal(this);
        online = true;
        SetRod(100);
        temp = 0;
    }
	///Sets the rod value and the rod text
    public void SetRod(float rod) {
        this.rod = rod;
        powerUnits = (int)(100 - rod) / 2;
        PowerSystem.setPower(powerUnits);
    }

	/// This function executes when the reactor needs to power up. settings all the ui elements and calls restore on the power system, so the power units can be allocated again.
    /// updates the boolean online to true to let the script know to heat things up again.
	public void powerUP() {
        if (temp <= 0 && PowerLines.getFixed()) {
            powerUnits = PowerSystem.restore();
            online = true;
            RecUI.restart.interactable = false;
            RecUI.shutdown.interactable = true;
            RecUI.controlRod.interactable = true;
        }
    }

	/// this funtion executes when the reactor powers down for any reason, through a restart or a overload.
    public void powerDown(bool crash) {
        if (crash) {
            Toast.addToast("Reactor overload\n Powering Down", 3);
            PowerLines.onBreak();
        }
        overload = crash;

        online = false;
        SetRod(100);
        powerUnits = PowerSystem.crash();

        RecUI.restart.interactable = true;
        RecUI.controlRod.interactable = false;
        RecUI.shutdown.interactable = false;
    }
 
    /// doUpdate is called once per frame
	/// This controls when the reactor will overload, the heating up and cooling down.
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
	/// Heating up functions used to increase the reactor temperature based on current power draw.
    public void heatingUp() {
        temp += (((Time.deltaTime + powerUnits / 4) *fillRate));
    }

	/// Cooling down variable controls how fast the reactor will cooldown after overload or powerdown.
    public void coolingDown() {
        int mod = 1;
        if (!overload) {
            mod = fillRateMult;
        }
        temp = temp <= 0 ? 0 : temp - DecRate * mod;
    }

	///function that is called when the reactor terminal is interacted with.
    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void onClose() {}
}
