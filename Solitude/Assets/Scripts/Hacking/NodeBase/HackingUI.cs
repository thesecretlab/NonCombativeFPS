using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HackingUI : MonoBehaviour {
    public Sprite Firewall;
    public Sprite systemcore;
    public Sprite IDS;
    Node[] nodes;
    public bool ishacked;
    HackingTerminal t;
    CountDownTimer timer;
    public void Hacked() {
        ishacked = true;
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
        if (ishacked) t.onFix();
        else t.hackFail();
    }
    public void reset() {
    }
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
