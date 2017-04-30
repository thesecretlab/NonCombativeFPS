using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    public Vector3 getPos() {
        return transform.position;
    }

	// Use this for initialization
	void Start () {
        this.setup();
        GameObject.FindGameObjectWithTag("Player").SendMessage("addInteractable", this);
	}

    protected abstract void setup();

    public abstract void interact();
}
