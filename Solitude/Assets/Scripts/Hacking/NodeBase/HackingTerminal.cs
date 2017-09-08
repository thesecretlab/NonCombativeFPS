using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTerminal : Terminal,Breakable {
    public override void interact() {
        
    }

    public void onBreak() {
        throw new NotImplementedException();
    }

    public void onFix() {
        throw new NotImplementedException();
    }

    protected override void doUpdate() {

    }

    protected override void initialise() {
        ui.GetComponent<HackingUI>().setTerminal(this);
    }
}
