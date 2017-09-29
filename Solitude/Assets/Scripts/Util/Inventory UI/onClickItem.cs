using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class onClickItem : MonoBehaviour {

	private Button self; 
	public int row;
	public int col;
	public Inventory parent;

	// Use this for initialization
	void Start () {
		self = this.GetComponent<Button>();
		self.onClick.AddListener (pressed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void pressed(){
		Debug.Log ("Button Pressed row:" + row + " col: " + col);
		parent.itemClicked(row,col);
	}


}
