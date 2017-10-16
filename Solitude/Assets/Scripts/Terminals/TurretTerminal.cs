using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTerminal : Terminal, Breakable {

	///Variable declaractions
    public bool doBreak;
    public float accuracy;
    public float decreaseRate;
    public float missAcc;
	
	///Turret UI object declaration.
    TurretUI UI;

	///Getter for accuracy variable.
    public float getAccuracy() {
        return accuracy;
    }
	/// This function is to adjust the accuracy in the terminal and set the accuracy in the GameConditions for use in other scripts.
    public void changeAccuracy(float change) {
        accuracy += change;
        GameConditions.setAccuracy(accuracy);
    }
	///function that triggers when the player interacts with the terminal
	///Shows the calibration UI.
    public override void interact() {
        missAcc = accuracy;
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

	///Adjust the accuracy of the turrets with each update.
    protected override void doUpdate() {
        if (doBreak) {
            doBreak = false;
            onBreak();
        }
        accuracy = Mathf.MoveTowards(accuracy, missAcc, decreaseRate * Time.deltaTime);
        GameConditions.setAccuracy(accuracy);
    }
	///Function for initialization.
    protected override void initialise() {
        new BreakEvent(this,10);
        UI = ui.GetComponent<TurretUI>();
        UI.setTerminal(this);
    }

    protected override void onClose() {

    }

    public void onBreak() {
        missAcc = accuracy - Random.Range(0,20);
    }

    public void onFix() {

    }
}
