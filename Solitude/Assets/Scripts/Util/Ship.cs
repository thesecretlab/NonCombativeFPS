using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#define MaxEmmisionGrab = 50;			//The Max ammount of materials that are grabbed intitally (NOT USED)

public class Ship : MonoBehaviour {

    public static Ship ship;
    Light[] lights;
	List<GameObject> floodlights = new List<GameObject>();
	//Material[] Emmision;			//Array of Material's with Emmission maps
	List<Material> Emmission = new List<Material>();

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
		Material[] reMats;
		Material[] Temp = new Material[300];		//A temporary array of all materials
		Texture test;								//A tenporary testing var
		int i = 0;									//Used to set the size of the Emissions Array
		//AnotheScript
		MaterialGlobalIlluminationFlags flags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        InvokeRepeating("tryBreak", waitSec, repeatSec);
        lights = GetComponentsInChildren<Light>();
		//floodlights = GameObject[].Find ("FloodLight****");

		foreach(GameObject obj in GameObject.FindObjectsOfType<GameObject>()){		//For each obj
			if (obj.name.Length >= 10) {
				if(obj.name.Substring(0,10).CompareTo("FloodLight") == 0){			//named FloodLight*****
					floodlights.Add(obj);											//add to floodlights
				}
			}
		}
		Debug.Log ("flood lights found: " + floodlights.Count);


		//getIlluminationMaterials ();
		/*
		AllRenderers = GetComponentsInChildren<Renderer>();						//Get all of the children's renderrs
		//foreach( Material me in mats){ This wont work
		foreach (Renderer re in AllRenderers) {									//For Each Renderer
			//Debug.Log("Test");
			//Tried "_EmissionMap"
			//test = re.material.GetTexture("_EmissionMap");					//Get Emission Map of Renderer
			if (re.material.globalIlluminationFlags.CompareTo(flags) == 0 && i < 1000) {	//If has map (test != null)
				Debug.Log("Height not equal to null || flag true");
				Temp [i] = re.material;											//Add material to Temp
				i++;
				if (i >= 90) {	
					Debug.Log ("Test"+i+" ");
				}
			}
			test = null;											
		}
		Debug.Log("New Emmission Array of size:"+i);
		Emmision = new Material[i];									//Create Emission Array to exact size
		while(i-1 >= 0){
			Emmision [i-1] = Temp [i-1];								//Copy from Temp to Emission
			i--;
		}
		 */

	}

    public void setPower(bool power) {
		
		//MaterialGlobalIlluminationFlags on = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		//MaterialGlobalIlluminationFlags off = MaterialGlobalIlluminationFlags.None;
		Color on = Color.white;
		Color off = Color.black;

        foreach (Light light in lights) {
            //light.enabled = power;
			/*
			 
			 */
        }
		/*
		if (power) {
			foreach (Material mat in Emmission) {
				//mat.globalIlluminationFlags = on;
				//mat.SetFloat("_Emission",2.0f);
				mat.SetColor("_EmissionColor",on);

			}
			Debug.Log ("Power On");
		} else {
			foreach (Material mat in Emmission) {
				//mat.globalIlluminationFlags = off;
				mat.SetColor("_EmissionColor",off);
				/*mat.SetFloat("_Emission",-1.0f);
				mat.SetFloat("Emission",-1.0f);
				mat.SetFloat("emission",-1.0f);
				mat.SetFloat("_Lightmapper",-1.0f);
				mat.SetFloat("Lightmapper",-1.0f);
			}
			Debug.Log ("Power Off");
		}*/
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

	public void getIlluminationMaterials(){
		MaterialGlobalIlluminationFlags flags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		Material[] temp;
		temp = (Material[]) Resources.FindObjectsOfTypeAll(typeof(Material));
		Debug.Log ("There are " + temp.Length+" Materials");

		for (int i = 0; i < temp.Length; i++) {
			if (temp[i].globalIlluminationFlags.CompareTo(flags) == 0) {	//If has map (test != null)
				temp[i].EnableKeyword("_EMISSION");
				Emmission.Add (temp [i]);
			}	
		}
		Debug.Log ("Found " + Emmission.Count + " Self Illuminating Materials");
		/*foreach (Material mat in Emmission) {
			mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
		}*/

	}
}
