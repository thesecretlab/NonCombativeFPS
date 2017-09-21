using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HackingTerminal : Terminal, Breakable {
    HackingUI hackUI;
    public override void interact() {
        showUI(true);
    }
    public void hackFail() {
        showUI(false);
        hackUI.reset();
    }
    public void onBreak() {
        throw new NotImplementedException();
    }
    public void onFix() {
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
