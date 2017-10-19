
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

/// 
/// \brief Interface for powered lights
/// 
public interface shipLight {
    /// 
    /// \brief Sets the power level of the light
    /// 
    /// \param [in] powerState The power level
    /// \return No return value
    /// 
    /// \details 
    /// 
    void setPower(int powerState);
}

/// 
/// \brief The ship master class
/// 
public class Ship : MonoBehaviour {
	/// 
	/// \brief If breakevents should happen
	/// 
    public bool doBreak;
	/// 
	/// \brief A class for the access denied panel
	/// 
    class AccessWindow : Window {
		/// The UI to be displayed
        GameObject ui;
        /// 
        /// \brief Constructor for the AccessWindow
        /// 
        /// \param [in] accessUI The UI for the window
        /// \return Returns the new AccessWindow
        /// 
        /// \details 
        /// 
        public AccessWindow(GameObject accessUI) {
            this.ui = accessUI;
        }
        /// 
        /// \brief Used to open the window
        /// 
        /// \return No return value
        /// 
        /// \details 
        /// 
        public void open() {
            Player.openWindow(this);
            ui.SetActive(true);
            Player.playerObj.FPSEnable(false);
        }
        /// 
        /// \brief Used to close the window
        /// 
        /// \return No return value
        /// 
        /// \details 
        /// 
        public void close() {
            Player.playerObj.FPSEnable(true);
            ui.SetActive(false);
        }
    }
	/// The static ship object
    public static Ship ship;
	/// A prefab for the access denied UI
    public GameObject AccessUIPrefab;
	/// The AccessUI gameObject created from the prefeb
    GameObject AccessUI;
	/// A window object of the accessUI
    AccessWindow AWin;

    //Light[] lights;

	//public List<GameObject> floodlights = new List<GameObject>();		//All child floodlights		
	//public List<GameObject> flurolights = new List<GameObject>();		//All child flurolights
	/// If the player has access to restricted terminals
    public bool access = true;
	/// A modifier to the break chance
    int breakmod = 0;
	/// A list of all the breakable objects
    List<BreakEvent> breakables = new List<BreakEvent>();
	/// How long the auto break system should wait before starting
    int waitSec = 1;
	/// How often the auto break system should break
    int repeatSec = 20;
    /// 
    /// \brief Used for initialization
    /// 
    /// \return No return value
    /// 
    /// \details Sets the static ship object. Will self remove if already set
    /// 
    void Awake() {
        if (ship != null) {
            Destroy(gameObject);
        } else {
            ship = this;
            //Debug.Log("Ship");
        }
    }
    /// 
    /// \brief Gets if the player has access
    /// 
    /// \return Returns if the player has access
    /// 
    /// \details 
    /// 
    public bool getAccess() {
        return access;
    }
    /// 
    /// \brief Sets the players access
    /// 
    /// \param [in] access If the player should have access
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void setAccess(bool access) {
        this.access = access;
    }
    /// 
    /// \brief Shows the access denied UI
    /// 
    /// \param [in] show If the UI should be shown or hidden
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void showAccess(bool show) {
        if (show) {
            AWin.open();
        } else {
            Player.closeWindow();
        }
        
    }
    /// 
    /// \brief Second initialization
    /// 
    /// \return No return value
    /// 
    /// \details Creates the access denied UI and window. Starts the auto break system
    /// 
    void Start () {
        AccessUI = Instantiate(AccessUIPrefab);
        AWin = new AccessWindow(AccessUI);
        AccessUI.GetComponentInChildren<Button>().onClick.AddListener(delegate { showAccess(false); });
        //Debug.LogWarning(name + ":" + uiPrefab.name);
        AccessUI.transform.SetParent(UICanvas.Canvas.transform, false);
        AccessUI.SetActive(false);

        InvokeRepeating("tryBreak", waitSec, repeatSec);
	}
    /// 
    /// \brief Triggers all break events
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void breakAll() {
        Debug.LogWarning("All breaking");
        foreach (BreakEvent e in breakables) {
            e.dobreak();
        }
    }
    /// 
    /// \brief Auto break system
    /// 
    /// \return No return value
    /// 
    /// \details Tests all breakevent percentages against a random number to trigger a break
    /// 
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
    /// 
    /// \brief Adds a breakevent object to the list
    /// 
    /// \param [in] e The break event
    /// \param [in] doBreak If it should start broken
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void addBreakEvent(BreakEvent e, bool doBreak) {
        breakables.Add(e);
        if (doBreak) e.dobreak();
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
