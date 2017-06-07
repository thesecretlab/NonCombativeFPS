using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public static Ship ship;
    Light[] lights;

    int breakmod = 0;

    List<BreakEvent> breakables = new List<BreakEvent>();
    int waitSec = 1;
    int repeatSec = 1000;
    // Use this for initialization
    void Awake() {
        if (ship != null) {
            Destroy(gameObject);
        } else {
            ship = this;
            Debug.Log("Ship");
        }
    }

    void Start () {
        InvokeRepeating("tryBreak", waitSec, repeatSec);
        lights = GetComponentsInChildren<Light>();
	}

    public void setPower(bool power) {
        foreach (Light light in lights) {
            light.enabled = power;
        }
    }

    void tryBreak() {
        Debug.Log("tryBreak");
        foreach(BreakEvent e in breakables) {
            if (Random.Range(1, breakmod) < e.breakPercent) {
                e.dobreak();
            }
        }
        breakmod = 100;
    }

    public void addBreakEvent(BreakEvent e) {
        //Debug.Log(e.breakPercent);
        breakables.Add(e);
    }
}
