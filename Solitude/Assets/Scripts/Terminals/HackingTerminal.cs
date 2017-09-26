using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HackingTerminal : Terminal, Breakable {
    HackingUI hackUI;

    public AudioSource IDSALERTsource;
    public AudioSource Firewallsource;

    public override void interact() {
        show();
    }
    public void hackFail() {
        hide();
        hackUI.reset();
    }
    public void onBreak() {
        Ship.ship.setAccess(false);
    }
    public void onFix() {
        Debug.Log("Fix");
        Ship.ship.setAccess(true);
        hackUI.reset();
        hide();
    }
    protected override void doUpdate() {
    }
    protected override void initialise() {
        hackUI = ui.GetComponent<HackingUI>();
        hackUI.setTerminal(this);
    }

    protected override void onClose() {
        hackUI.reset();
    }
}
