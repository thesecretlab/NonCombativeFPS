using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTL : MonoBehaviour, PowerConsumer {
    public void updatePower(bool powered) {
        GameConditions.setSpeed(powered);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
