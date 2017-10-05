using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTerminal : Terminal, Breakable {

    public float accuracy;
    public float decreaseRate;
    float missAcc;
    TurretUI UI;

    public float getAccuracy() {
        return accuracy;
    }

    public void changeAccuracy(float change) {
        accuracy += change;
        GameConditions.setAccuracy(accuracy);
    }

    public override void interact() {
        missAcc = accuracy;
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void doUpdate() {
        accuracy = Mathf.MoveTowards(accuracy, missAcc, decreaseRate * Time.deltaTime);
        GameConditions.setAccuracy(accuracy);
    }

    protected override void initialise() {
        new BreakEvent(this,10);
        accuracy = 0;
        UI = ui.GetComponent<TurretUI>();
        UI.setTerminal(this);
    }

    protected override void onClose() {

    }

    public void onBreak() {
        missAcc = accuracy - Random.Range(0,-20);
    }

    public void onFix() {

    }
}
