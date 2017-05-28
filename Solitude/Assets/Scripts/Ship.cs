using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    List<BreakEvent> breakables = new List<BreakEvent>();
    int waitSec = 10;
    int repeatSec = 10;
	// Use this for initialization
	void Start () {
        InvokeRepeating("tryBreak", waitSec, repeatSec);
	}

    void trybreak() {
        foreach(BreakEvent e in breakables) {
            if (Random.Range(1, 100) > e.breakPercent) {
                e.dobreak();
            }
        }
    }

    public void addBreakEvent(BreakEvent e) {
        breakables.Add(e);
        e.dobreak();
    }
}
