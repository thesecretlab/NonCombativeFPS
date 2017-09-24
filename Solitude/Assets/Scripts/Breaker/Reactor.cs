using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour, Breakable {
    public Lever[] levers;

    public void onBreak() {
        PowerSystem.powersystem.crash();
        Debug.Log("boom");
        foreach(Lever lev in levers) {
            //Debug.Log(lev.getName());
            lev.blow();
        }
    }

    public void onFix() {
        Debug.Log("Breakers Fixed");
        PowerSystem.powersystem.restore();
    }

    public void throwLever(int lever) {
        bool isblown = false;
        foreach (Lever lev in levers) {
            if (!isblown) {
                if (lev.isBlown()) {
                    isblown = true;
                }
            }
        }
        if (!isblown) {
            onFix();
        }
    }

    void Start () {
        new BreakEvent(this, 10);
		levers = GetComponentsInChildren<Lever>();
        int i = 0;
        foreach (Lever lev in levers) {
            lev.setReactor(this,i);
            i++;
        }
	}
}
