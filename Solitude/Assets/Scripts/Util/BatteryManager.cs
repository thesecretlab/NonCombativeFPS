﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour {

    //these had to be floats due to waitforsecondsrealtime being used
    public static float MaxBattery = 100; //enables future tweaking
    public float BatteryRemaining; //public to enable modification by other game objects
    public float CurrentDrain = 1; //the ammount to drain every X seconds
    public float DrainRate = 3; //number of seconds to increment the remaining battering by currentdrain
    public GameObject torchLight;
    public Slider batterySlider; //ui object to send info to, currently a slider

    public void RechargeBattery() //enables charging by other objects, namely charging stations
    {
        Debug.Log("charged");
        BatteryRemaining = MaxBattery;
    }

    IEnumerator StartDraining()
    {
        while (true)
        {
            if (Time.timeScale == 0) //game is currently paused
            {
                CurrentDrain = 0;
            }
            else if (torchLight.activeInHierarchy == true)
            {
                CurrentDrain = 2;
            }
            else
            {
                CurrentDrain = 1;
            }

            if (BatteryRemaining <= 0)
            {
                Debug.Log("Player battery is flat, rip");
                torchLight.SetActive(false);
            }

            BatteryRemaining = BatteryRemaining - CurrentDrain;
            batterySlider.value = BatteryRemaining;
            yield return new WaitForSecondsRealtime(DrainRate);
            
        }
    }

	// Use this for initialization
	void Start () {
        RechargeBattery();
        StartCoroutine(StartDraining());
	}
}
