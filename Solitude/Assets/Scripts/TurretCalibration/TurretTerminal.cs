using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTerminal : Terminal {
    public override void interact() {
        showUI(true);
    }

    protected override void doUpdate() {
        
    }

    protected override void initialise() {
        
    }

    protected override void onClose() {
        showUI(false);
    }
}
