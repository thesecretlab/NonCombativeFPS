using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorTerminal : Terminal {
   
    
    
	///Powerlines object declaration
	public PowerLines PowerLines;

    
	/// Reactor UI object declaration, used to change the Ui elements.
	ReactorUI RecUI;
	///Reactor Terminal object declaration, used to reference the Reactorterminal objects.
    public static ReactorTerminal reactorObj;

	///Current Power units available.
    public int powerUnits; 
    ///if the true the reactor is functioning and online, if false it is offline.
	bool online; 
	/// This is triggered when the reactor overloads.
    bool overload; 
	///The heating up fill rate, can be adjusted for faster or slower times.
    public float fillRate = 0.00001f; 
	///The decrease rate of the temperature when the reactor is cooling.
    float DecRate = 0.1f; 
	///Fill rate multiplier is used when the reactor overloads
    int fillRateMult = 2;
	///do break is called when the the ship is trying to break the system.
	public bool doBreak; 
	///Stores the current reactor temperature.
    private float temp; 
	 ///stores the current insertion value of the control rod.
    private float rod;
    
    /// Use this for initialization when the script is first accessed
    void Awake() {
		///Assigns the reactorobj to this if it is null.
        if (reactorObj == null) {
            reactorObj = this;
        } else {
            Destroy(transform.gameObject);
        }
    }

	///Returns the current reactor temperature.
    public float getTemp() {
        return temp < 0 ? 0 : temp;
    }
	///Returns the current rod insertion value.
    public float getRod() {
        return rod;
    }
	///Returns the current power units available.
    public int getPow() {
        return powerUnits;
    }
	///returns the 0 if online is true and temp if it isn't
    public float getTime() {
        return online ? 0 : temp;
    }
	/// This function monitors and updates the reactor status as things change through out the play.
    public string getStatus() {
       
	///if the reactor is offline and overload return overloaded otherwise return Flushing.
	   if (!online) {
            if (getTime() != 0) {
                if (overload) {
                    return "Overloaded";
                } else {
                    return "Flushing";
                }
            }
			/// If the powerlines are damaged return Power Lines disconnected
            if (!PowerLines.getFixed()) {
                return "Power Lines Disconnected";
            }
            return "Ready";
        }
		///returns the reactor load depending on the current power unit value.
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
        ///Gets the current ReactorUI component, User interface.
		RecUI = ui.GetComponent<ReactorUI>();
        ///Sets the terminal to the RecUi that was just got.
		RecUI.setTerminal(this);
		///Sets the reactor to online.
        online = true;
		/// sets the rod value to 100 and changes all UI elements linked to it.		
        SetRod(100); 
		///Sets temp to 0.
        temp = 0;
    }
	///Sets the rod value and the rod text
    public void SetRod(float rod) {
        ///sets the rod value to be the current instances of rod.
		this.rod = rod;
		/// sets the power units to 100 - rod.
        powerUnits = (int)(100 - rod) / 2; 
		///Sets the current draw in the power system to be the power units.
        PowerSystem.setPower(powerUnits);
    }

	/// This function executes when the reactor needs to power up. settings all the ui elements and calls restore on the power system, so the power units can be allocated again.
    /// updates the boolean online to true to let the script know to heat things up again.
	public void powerUP() {
        ///when the power lines are fixed and the temp is 0.
		if (temp <= 0 && PowerLines.getFixed()) {
			///restores the power system and sets the power units avaiable to be what they were.
            powerUnits = PowerSystem.restore();
            /// sets the reactor to online
			online = true;
			///Sets the restart button to not be interactable
            RecUI.restart.interactable = false; 
			
            ///Sets the shutdown button to be interactable.
			RecUI.shutdown.interactable = true; 
			///Sets the controlRod to be interactable,
            RecUI.controlRod.interactable = true; 
        }
    }

	/// this funtion executes when the reactor powers down for any reason, through a restart or a overload.
    public void powerDown(bool crash) {
        
		///when the reactor crashes send a message of what it has done.
		if (crash) {
			///sends the message to the top of the players screen
            Toast.addToast("Reactor overload\n Powering Down", 3);
            PowerLines.onBreak(); ///breaks the powerlines.
        }
		///sets the overload to crash, being true.
        overload = crash;
		///Sets reactor to offline
        online = false;
		///calls set rod with the value 100
        SetRod(100); 
		///sets the powerunits to what the should be when crashes.
        powerUnits = PowerSystem.crash();
		/// Sets the reatart to be interactable
        RecUI.restart.interactable = true;
		///sets the controlRod to not be interactable.
        RecUI.controlRod.interactable = false;
		///sets the shutdown to not be interactable.
        RecUI.shutdown.interactable = false;
    }
 
    /// doUpdate is called once per frame
	/// This controls when the reactor will overload, the heating up and cooling down.
    protected override void doUpdate() {
		
		///if it is broken fix it and set the temp to 100
        if (doBreak) {
            doBreak = false;
            temp = 100;
        }
		///if the temp is 100 or more, powerdown the reactor.
        if (temp >= 100) {
            powerDown(true);
        }
		///if the reactor is online and generating power, call heating up.
        if (online && powerUnits != 0) {
            heatingUp();///heat up reactor
        } else {
            coolingDown();///cool down reactor.
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
        
		///if get access is returns true, show the UI for the reactor. If false show error on terminal screen.
		if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void onClose() {}
}
