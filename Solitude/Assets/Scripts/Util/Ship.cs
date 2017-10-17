
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
    public bool doBreak;

    class AccessWindow : Window {
        GameObject ui;
        private GameObject accessUI;

        public AccessWindow(GameObject accessUI) {
            this.ui = accessUI;
        }

        public void open() {
            Player.openWindow(this);
            ui.SetActive(true);
            Player.playerObj.FPSEnable(false);
        }
        public void close() {
            Player.playerObj.FPSEnable(true);
            ui.SetActive(false);
        }
    }

    public static Ship ship;
    public GameObject AccessUIPrefab;
    GameObject AccessUI;
    AccessWindow AWin;

    //Light[] lights;

	//public List<GameObject> floodlights = new List<GameObject>();		//All child floodlights		
	//public List<GameObject> flurolights = new List<GameObject>();		//All child flurolights

    public bool access = true;

    int breakmod = 0;

    List<BreakEvent> breakables = new List<BreakEvent>();
    int waitSec = 1;
    int repeatSec = 20;
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
        if (show) {
            AWin.open();
        } else {
            Player.closeWindow();
        }
        
    }

    void Start () {
        AccessUI = Instantiate(AccessUIPrefab);
        AWin = new AccessWindow(AccessUI);
        AccessUI.GetComponentInChildren<Button>().onClick.AddListener(delegate { showAccess(false); });
        //Debug.LogWarning(name + ":" + uiPrefab.name);
        AccessUI.transform.SetParent(UICanvas.Canvas.transform, false);
        AccessUI.SetActive(false);

        InvokeRepeating("tryBreak", waitSec, repeatSec);
	}

    void breakAll() {
        Debug.LogWarning("All breaking");
        foreach (BreakEvent e in breakables) {
            e.dobreak();
        }
    }

    void tryBreak() {
        if (doBreak) {
            Debug.Log("tryBreak");
            foreach (BreakEvent e in breakables) {
                if (Random.Range(1, 100) < e.breakPercent) {
                    e.dobreak();
                }
            }
        }
    }

    public void addBreakEvent(BreakEvent e, bool b) {
        breakables.Add(e);
        if (b) e.dobreak();
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
