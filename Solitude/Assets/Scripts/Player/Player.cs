using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {
    public static bool pausable;
    public static Player playerObj;
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable active;
    private bool canInteract = false;
    public bool FPSActive = true;
    private RigidbodyFirstPersonController _FPSController;
    public GameObject interactableText; //
    private RigidbodyFirstPersonController getFPSControler() {
        if (_FPSController == null) {
            _FPSController = GetComponent<RigidbodyFirstPersonController>();
        }
        return _FPSController;
    }
    void Awake() {
        if (playerObj == null) {
            playerObj = this;
        } else {
            Destroy(transform.gameObject);
        }
    }
    // Use this for initialization
    void Start() {
        pausable = true;
        interactableText.SetActive(false); //hides the ui text on starting the game
    }
    public void FPSEnable(bool enable) {
        FPSActive = enable;
        getFPSControler().enabled = enable;
        getFPSControler().mouseLook.SetCursorLock(enable);
    }
    private void doInteract() {
        if (FPSActive && canInteract) {
            active.interact();
        }
    }
    // Update is called once per frame
    void Update() {
        checkInteractables();
        if (CrossPlatformInputManager.GetButtonDown("Interact")) {
            doInteract();
        }
    }
    void checkInteractables() {
        int reach = 3; //distance that the player can interact with an object
        bool interact = false;
        foreach (Interactable inter in interactables) {
            //Only continuees checking if no interactable has been found;
            //Debug.Log("1");
            if (!interact) {
                //checks if the interactable is active
                //Debug.Log("2");
                if (inter.isActive()) {
                    //checks if the interactable is within the players reach
                    //Debug.Log("3");
                    if (Vector3.Distance(inter.getPos(), transform.position) < reach) {
                        //checks if the player is looking at the interactable
                        //Debug.Log("4");
                        if (lookingAt(inter)) {
                            //Debug.Log("5");
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
    public void addInteractable(Interactable inter) {
        interactables.Add(inter);
        //inter.interact();
    }
    public static void allowPause(bool allow) {
        pausable = allow;
    }
}