using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Controls the functionality of the hacking terminal
///Fixes the terminal lock out problem.
///</summary>
///<remarks>
///Authors: Jeffrey Albion, Jonothan Nield.
///</remarks>

public class HackingTerminal : Terminal, Breakable {
    
	///HackingUI variable to access hackinUI class variables.
	HackingUI hackUI;

    ///Variable for storing an IDSalert audio source.
	public AudioSource IDSALERTsource;
	///Variavle for storing firewall audio source.
    public AudioSource Firewallsource;

    ///on interaction show interface.
	public override void interact() {
        show();
    }
	///when timer runs out hide the interace and resest the hacking task.
    public void hackFail() {
        hide();
        hackUI.reset();
    }
	
	///onbreak show access denied message.
    public void onBreak() {
        Debug.Log("Locked out");
        Ship.ship.setAccess(false);
    }
	///when fixed restore access and reset hacking minigame and hide UI.
    public void onFix() {
        Debug.Log("Fix");
        Ship.ship.setAccess(true);
        hackUI.reset();
        hide();
    }
    protected override void doUpdate() {
    }
	
	///Intialises breakevent and get the hackUI and initialize it.
    protected override void initialise() {
        new BreakEvent(this, 10, true); //set back to 10 post testing
        hackUI = ui.GetComponent<HackingUI>();
        hackUI.setTerminal(this);
    }
	
	///Onclose reset hacking minigame.
    protected override void onClose() {
        hackUI.reset();
    }
}
