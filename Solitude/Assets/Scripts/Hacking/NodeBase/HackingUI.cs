using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// \brief Interface between the Hacking UI and the Terminal
/// 
public class HackingUI : MonoBehaviour {
    
	///variable for storing the firewall sprite.
	public Sprite Firewall;
	///variable for storing the systemcore sprite.
    public Sprite systemcore;
	///variable for storing the IDS sprite
    public Sprite IDS;
	///variable for storing the nodes.
    Node[] nodes;
	///variable for storing if the system is hacked or not.
    public bool ishacked;
	///variable for storing the hacking terminal object.
    HackingTerminal t;
	///variable for storing the countdown to failure timer.
    CountDownTimer timer;
	
	///when the a terminal gets hacked set is hacked to true.
    public void Hacked() {
        ishacked = true;
        Debug.Log("Hacked");
        timer.Hacked();
    }
	///returns the hacking terminal.
    public HackingTerminal getTerminal() {
        return t;
    }
	///sets timer IDSclicked variable.
    public void IDSclicked()
    {
        timer.IDSclicked();
    }
	///sets the timer Firewall click varaible.
    public void Firewallclicked()
    {
        timer.Firewallclicked();
    }
	
	///If the ishacked is true fix the terminal or call hack failed.
    public void doneTimer() {
        if (ishacked) t.onFix();
        else t.hackFail();
    }
	
	///Resets the hacking minigame returning it to a fresh state.
    public void reset() {
        timer.reset();
        foreach (Node n in nodes) {
            n.setUI(this);
            n.close();
        }
        nodes[0].open();
        ishacked = false;
    }
	
	///Initialises all nodes in the hacking minigame.
    void Start() {
        nodes = gameObject.GetComponentsInChildren<Node>();
        foreach (Node n in nodes) {
            n.setUI(this);
            n.close();
        }
        nodes[0].open();
        timer = GetComponentInChildren<CountDownTimer>();
        timer.setUI(this);
    }
    public void setTerminal(HackingTerminal t) {
        this.t = t;
    }
}
