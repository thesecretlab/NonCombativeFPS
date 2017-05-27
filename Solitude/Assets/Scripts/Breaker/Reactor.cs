using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour, Breakable {
    public Lever[] levers;

    public void onBreak() {
        throw new NotImplementedException();
    }

    public void onFix() {
        throw new NotImplementedException();
    }

    public void throwLever(int lever) {
        levers[lever].gameObject.SetActive(false);
    }

    void Start () {
		levers = GetComponentsInChildren<Lever>();
        int i = 0;
        foreach (Lever lev in levers) {
            lev.setReactor(this,i);
            i++;
            Debug.Log(i);
        }
	}
}
