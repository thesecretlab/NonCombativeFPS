using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
///controls the speed of the ftl
///indirectly controls the speed of the goal timer.
///</summary>
///<remarks>
/// Authors:
///</remarks>
public class FTL : MonoBehaviour, PowerConsumer {
    
	
	public void updatePower(bool powered) {
        ///set the speeded if powered.
		GameConditions.setSpeed(powered);
    }

    /// Use this for initialization
    void Start () {
		
	}
	
	/// Update is called once per frame
	void Update () {
		
	}
}
