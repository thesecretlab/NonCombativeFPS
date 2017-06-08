using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#define MaxEmmisionGrab = 50;			//The Max ammount of materials that are grabbed intitally (NOT USED)

public class Ship : MonoBehaviour {

    public static Ship ship;
    Light[] lights;
	Material[] Emmision;			//Array of Material's with Emmission maps

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
		Renderer[] AllRenderers;					//An array of all objects renderes
		Material[] Temp = new Material[200];		//A temporary array of all materials
		Texture test;								//A tenporary testing var
		int i = 0;									//Used to set the size of the Emissions Array

        InvokeRepeating("tryBreak", waitSec, repeatSec);
        lights = GetComponentsInChildren<Light>();
		/*
		AllRenderers = GetComponentsInChildren<Renderer>();			//Get all of the children's renderrs
		foreach (Renderer re in AllRenderers) {						//For Each Renderer
			//Debug.Log("Test");
			test = re.material.GetTexture("_EmissionMap");			//Get Emission Map of Renderer
			if (test != null) {										//If has map
				Debug.Log("Height not equal to null");
				Temp [i] = re.material;								//Add material to Temp
				i++;
				if (i >= 90) {	
					Debug.Log ("Test"+i+" ");
				}
			}
			test = null;											
		}
		Debug.Log("New Emmission Array of size:"+i);
		Emmision = new Material[i];									//Create Emission Array to exact size
		while(i >= 0){
			Emmision [i] = Temp [i];								//Copy from Temp to Emission
		}
		 
		*/
	}

    public void setPower(bool power) {
        foreach (Light light in lights) {
            light.enabled = power;
			/*
			 
			 */
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
