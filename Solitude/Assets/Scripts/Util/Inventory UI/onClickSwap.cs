using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class onClickSwap : MonoBehaviour {

	private Button self;
	public ItemSwitching parent;
	public bool swap = false;

	// Use this for initialization
	void Start () {
		self = this.GetComponent<Button>();
		self.onClick.AddListener (pressed);
	}

	// Update is called once per frame
	void Update () {

	}

	void pressed(){
		//Debug.Log ("Button Pressed Swap");
		if (swap) {
			swap = false;
		} else {
			swap = true;
		}
		//parent.swap();
	}
}
