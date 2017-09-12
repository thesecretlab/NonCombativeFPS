using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class onClickSwap : MonoBehaviour {

	private Button self;
	public ItemSwitching parent;

	// Use this for initialization
	void Start () {
		self = this.GetComponent<Button>();
		self.onClick.AddListener (pressed);
	}

	// Update is called once per frame
	void Update () {

	}

	void pressed(){
		Debug.Log ("Button Pressed Swap");
		//parent.swap();
	}
}
