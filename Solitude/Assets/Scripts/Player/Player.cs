using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

/// \brief Interface for windows
public interface Window {
    void close();
}

/// 
/// \brief The class holding all player systems
/// 
public class Player : MonoBehaviour {
	/// A stack of all open windows
    public Stack<Window> windows;
	/// Static player used for calling
    public static Player playerObj;
	/// A list of all interactable objects
    private List<Interactable> interactables = new List<Interactable>();
	/// The current active interactable
    private Interactable active;
	/// If the player can currently interact with an object
    private bool canInteract = false;
	/// If the fps controller is currently active
    public bool FPSActive = true;
	/// A reference to the FPS controller
    private RigidbodyFirstPersonController _FPSController;
	/// A reference to the text to display when an object is interactable
    public GameObject interactableText; //

    #region Static
    /// 
    /// \brief Called to close the last opened window
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    public static void closeWindow() {
        playerObj._closeWindow();
    }
    /// 
    /// \brief Used to open a window
    /// 
    /// \param [in] win The window that has been opened
    /// \return No return value
    /// 
    /// \details 
    /// 
    public static void openWindow(Window win) {
        Debug.Log("Window");
        playerObj._openwindow(win);
    }
    #endregion
    /// 
    /// \brief Called when the escape key is pressed
    /// 
    /// \return No return value
    /// 
    /// \details Will close the top most window or pause if no window is open
    /// 
    public void onEsc() {
       if (windows.Count > 0) {
            _closeWindow();
        } else {
            PauseScript.Pause();
        }
    }
    /// 
    /// \brief Used to close the top most open window
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void _closeWindow() {
        if (windows.Count > 0) {
            windows.Pop().close();
        }
    }
    /// 
    /// \brief Used to set a new open window
    /// 
    /// \param [in] win The newly opened window
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void _openwindow(Window win) {
        windows.Push(win);
    }
    /// 
    /// \brief Gets and sets the reference to the FPScontroller
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    private RigidbodyFirstPersonController getFPScontroller() {
        if (_FPSController == null) {
            _FPSController = GetComponent<RigidbodyFirstPersonController>();
        }
        return _FPSController;
    }
    /// 
    /// \brief First initialization
    /// 
    /// \return No return value
    /// 
    /// \details Sets the static Player object. Will self remove if the static player is already set
    /// 
    void Awake() {
        if (playerObj == null) {
            playerObj = this;
        } else {
            Destroy(transform.gameObject);
        }
    }
    /// 
    /// \brief Second initialization
    /// 
    /// \return No return value
    /// 
    /// \details Sets up the Window stack
    /// 
    void Start() {
        windows = new Stack<Window>();
        interactableText.SetActive(false); //hides the ui text on starting the game
    }
    /// 
    /// \brief Enables/disables the FPScontroller
    /// 
    /// \param [in] enable If the FPScontroller should be enabled
    /// \return No return value
    /// 
    /// \details Also unlocks the mouse
    /// 
    public void FPSEnable(bool enable) {
        FPSActive = enable;
        getFPScontroller().enabled = enable;
        getFPScontroller().mouseLook.SetCursorLock(enable);
    }
    /// 
    /// \brief Calls the interact function of the current interactable object
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    private void doInteract() {
        if (FPSActive && canInteract) {
            active.interact();
        }
    }
    /// 
    /// \brief Called once per frame
    /// 
    /// \return No return value
    /// 
    /// \details Calls for an interactable check. Checks for button presses.
    /// 
    void Update() {
        checkInteractables();
        if (CrossPlatformInputManager.GetButtonDown("Interact")) {
            doInteract();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            onEsc();
        }
    }
    /// 
    /// \brief Checks if any interactable's are interactable
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    void checkInteractables() {
        int reach = 3; //distance that the player can interact with an object
        bool interact = false;
        foreach (Interactable inter in interactables) {
            //Only continues checking if no interactable has been found;
            if (!interact) {
                //checks if the interactable is active
                if (inter.isActive()) {
                    //checks if the interactable is within the players reach
                    if (Vector3.Distance(inter.getPos(), transform.position) < reach) {
                        //checks if the player is looking at the interactable
                        if (lookingAt(inter)) {
                            active = inter;
                            interact = true;
                        }
                    }
                }
            }
        }
        if (interact) {
            canInteract = true;
            interactableText.SetActive(true);//
        } else {
            canInteract = false;
            interactableText.SetActive(false);//
        }
    }
    /// 
    /// \brief Function to if the player is facing an object
    /// 
    /// \param [in] inter The interactable to check
    /// \return Returns if the player is looking at the interactable
    /// 
    /// \details 
    /// 
    private bool lookingAt(Interactable inter) {
        int look = 20; //max angle between the players look direction and the object;
        //a point representing the interactables position relative to the player
        Vector3 targetDir = inter.getPos() - transform.position;
        //The target direction reduces to 2D plane
        Vector2 targetDirRed = new Vector2(targetDir.x, targetDir.z);
        //Players forward reduced to 2D plane;
        Vector2 playerDirRed = new Vector2(transform.forward.x, transform.forward.z);
        //returns true if the angle between the target dir and the player forward is less then the look angle
        return Vector2.Angle(targetDirRed, playerDirRed) < look;
    }
    /// 
    /// \brief Adds an interactable to the list
    /// 
    /// \param [in] inter The interactable to be added
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void addInteractable(Interactable inter) {
        interactables.Add(inter);
        //inter.interact();
    }
}