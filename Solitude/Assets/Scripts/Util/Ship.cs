using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*		Main controller for game functions
 * 		
 * 		*INSERT ALL INFORMATION HERE*
 * 		
 * 		Created By Jeffery Albion
 * 		Modified By Alexander Tilley (Last Edit: 12/08/2017)
 */

public interface shipLight {
    void setPower(int powerState);
}

public class Ship : MonoBehaviour {

    public static Ship ship;
    public GameObject AccessUIPrefab;
    GameObject AccessUI;

    Light[] lights;

	public List<GameObject> floodlights = new List<GameObject>();		//All child floodlights		
	public List<GameObject> flurolights = new List<GameObject>();		//All child flurolights

    public bool access;

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
            //Debug.Log("Ship");
        }
    }

    public bool getAccess() {
        return access;
    }

    public void setAccess(bool access) {
        this.access = access;
    }

    public void showAccess(bool show) {
        AccessUI.SetActive(show);
        Player.playerObj.FPSEnable(!show);
    }

    void Start () {
        AccessUI = Instantiate(AccessUIPrefab);
        AccessUI.GetComponentInChildren<Button>().onClick.AddListener(delegate { showAccess(false); });
        //Debug.LogWarning(name + ":" + uiPrefab.name);
        AccessUI.transform.SetParent(UICanvas.Canvas.transform, false);
        AccessUI.SetActive(false);

        InvokeRepeating("tryBreak", waitSec, repeatSec);
        lights = GetComponentsInChildren<Light>();			//TODO NOT NEEDED?

		foreach(GameObject obj in GameObject.FindObjectsOfType<GameObject>()){		//For each obj
			if (obj.name.Length >= 10) {
				if(obj.name.Substring(0,10).CompareTo("FloodLight") == 0){			//named FloodLight*****
					floodlights.Add(obj);											//add to floodlights
				}
				if(obj.name.Substring(0,10).CompareTo("FluroLight") == 0){			//named FluroLight*****
					flurolights.Add(obj);											//add to FluroLight
				}
			}
		}
		//Debug.Log ("flood lights found: " + floodlights.Count);
		//Debug.Log ("fluro lights found: " + flurolights.Count);

		/* 										KEEP ME JUST IN CASE -ALEX
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

    /*public void setPower(bool power) {

		foreach (GameObject light in floodlights) {						//ForEach FloodLight
			FloodLight[] script = light.GetComponents<FloodLight>();
			if (script.Length >= 0) {
				script [0].setPower (power);							//Change Power State
			}
        }
		foreach (GameObject light in flurolights) {						//ForEach FluroLight
			FluroLight[] script = light.GetComponents<FluroLight>();
			if (script.Length >= 0) {
				script [0].setPower (power);							//Change power state
			}
		}
    }*/

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

	/*											KEEP ME JUST IN CASE -ALEX
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
		}

	} */
}
