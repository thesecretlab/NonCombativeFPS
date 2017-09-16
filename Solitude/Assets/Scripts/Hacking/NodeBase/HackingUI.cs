using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingUI : MonoBehaviour {
    public Sprite Firewall;
    public Sprite systemcore;
    public Sprite IDS;

    HackingTerminal t;
    CountDownTimer timer;


    public void Hacked() {
        timer.Hacked();
    }

    public void IDSclicked()
    {
        timer.IDSclicked();
    }

    public void Firewallclicked()
    {
        timer.Firewallclicked();
    }

    public void doneTimer() {
        Debug.LogError("Hack Closing: Not Implemented");
    }

    void Start() {
        foreach (Node n in gameObject.GetComponentsInChildren<Node>()) {
            n.setUI(this);
        }
        timer = GetComponentInChildren<CountDownTimer>();
        timer.setUI(this);
    }

    public void setTerminal(HackingTerminal t) {
        this.t = t;
    }
}
