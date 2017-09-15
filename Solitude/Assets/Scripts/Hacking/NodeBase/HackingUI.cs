using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingUI : MonoBehaviour {
    public Sprite Firewall;
    public Sprite systemcore;
    public Sprite IDS;

    HackingTerminal t;

    void doneHacking() {
        t.onFix();
    }

    void Start() {
        foreach (Node n in gameObject.GetComponentsInChildren<Node>()) {
            n.setUI(this);
        }
    }

    public void setTerminal(HackingTerminal t) {
        this.t = t;
    }
}
