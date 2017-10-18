using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTerminal : Terminal, Breakable {

	///Variable declaractions
    public bool doBreak;///tracks if the system is broken or not.
    public float accuracy; ///Stores the current accuracy.
    public float decreaseRate; ///The rate at which the accuracy will decrease.
    public float missAcc; ///stores the chance the turrent will miss.
	
	///Turret UI object declaration.
    TurretUI UI;

	///Getter for accuracy variable.
    public float getAccuracy() {
        return accuracy;
    }
	/// This function is to adjust the accuracy in the terminal and set the accuracy in the GameConditions for use in other scripts.
    public void changeAccuracy(float change) {
        accuracy += change; ///adds the change value to the current accuracy.
        GameConditions.setAccuracy(accuracy); ///Sets the accuracy in the game conditions object.
    }
	///function that triggers when the player interacts with the terminal
	///Shows the calibration UI.
    public override void interact() {
        missAcc = accuracy; ///sets the missAcc to the current accuracy.
        
		///if get access returns false show error message, if true show minigame.
		if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

	///Adjust the accuracy of the turrets with each update.
    protected override void doUpdate() {
        
		///If its broken fix it and call onbreak.
		if (doBreak) {
            doBreak = false;
            onBreak();
        }
        accuracy = Mathf.MoveTowards(accuracy, missAcc, decreaseRate * Time.deltaTime); ///set the accuracy value based of delta time, so its unaffected by framerates.
        GameConditions.setAccuracy(accuracy); ///Sets the accuracy in the gameconditions object.
    }
	///Function for initialization.
    protected override void initialise() {
        new BreakEvent(this,10); ///createss a new break event
        UI = ui.GetComponent<TurretUI>(); ///gets the current turret UI and stores it in UI
        UI.setTerminal(this); ///Sets the Terminal to be the current object.
    }

    protected override void onClose() {

    }

    public void onBreak() {
        missAcc = accuracy - Random.Range(0,20); ///when it breaks lower the accuracy by a random amount.
    }

    public void onFix() {

    }
}
