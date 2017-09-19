using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Interactable : MonoBehaviour {
    public bool active = true;
    public Vector3 getPos() {
        return transform.position;
    }
	// Use this for initialization
	void Start () {
        this.setup();
        Player.playerObj.addInteractable(this);
	}
    public bool isActive() {
        return active;  
    }
    public void setActive(bool active) {
        this.active = active;
    }
    protected abstract void setup();
    public abstract void interact();
}