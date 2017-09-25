using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminal : Terminal {
    public override void interact() {
        if (Ship.ship.getAccess()) {
            showUI(true);
        } else {
            Ship.ship.showAccess(true);
        }
    }

    protected override void doUpdate() {
        
    }

    protected override void initialise() {
        
    }
    protected override void onClose() {
        showUI(false);
    }
}
