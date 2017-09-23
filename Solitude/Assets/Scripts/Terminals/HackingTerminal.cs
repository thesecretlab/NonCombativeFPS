using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HackingTerminal : Terminal, Breakable {
    HackingUI hackUI;

    public AudioSource IDSALERTsource;
    public AudioSource Firewallsource;

    public override void interact() {
        showUI(true);
    }
    public void hackFail() {
        showUI(false);
        hackUI.reset();
    }
    public void onBreak() {
        Ship.ship.setAccess(false);
    }
    public void onFix() {
        Ship.ship.setAccess(false);
        hackUI.reset();
        showUI(false);
    }
    protected override void doUpdate() {
    }
    protected override void initialise() {
        hackUI = ui.GetComponent<HackingUI>();
        hackUI.setTerminal(this);
    }

    protected override void onClose() {
        hackUI.reset();
        showUI(false);
    }
}
