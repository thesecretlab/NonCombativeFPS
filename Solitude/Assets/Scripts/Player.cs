using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    List<Interactable> interactables = new List<Interactable>();
    Interactable active;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void checkInteractables() {
        int reach = 3; //distance that the player can interact with an object
        bool interact = false;
        foreach (Interactable inter in interactables) {
            //Only continuees checking if no interactable has been found;
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
            //TODO code for showing "E to interact"
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

    void addInteractable(Interactable inter) {
        interactables.Add(inter);
        inter.interact();
    }
}
