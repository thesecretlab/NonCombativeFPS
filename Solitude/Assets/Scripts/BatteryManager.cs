using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour {

    public static float MaxBattery = 100; //enables future tweaking
    public float BatteryRemaining; //public to enable modification by other game objects
    public float CurrentDrain = 1; //the ammount to drain every X seconds
    public float DrainRate = 5; //number of seconds to increment the remaining battering by currentdrain
    float timer;

    public void RechargeBattery() //enables charging by other objects, namely charging stations
    {
        BatteryRemaining = MaxBattery;
    }

    //handles the battery draining
    IEnumerator StartDraining()
    {
        while (true)
        {
            BatteryRemaining = BatteryRemaining - CurrentDrain;
            yield return new WaitForSecondsRealtime(DrainRate);
        }
    }

	// Use this for initialization
	void Start () {
        RechargeBattery();
        StartCoroutine(StartDraining());
	}
}
