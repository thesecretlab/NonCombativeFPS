using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Used to call the Inventory.itemClicked() function when the player clicks the button this script is attached to.
/// This script is used as a part of the player inventory. It is attached to each item in the inventory and set up by Inventory
/// so that when the player clicks an item; this script sends the row and column information to the Inventory so it can perform 
/// opperations.
/// </summary>
///<remarks> 
/// By Alexander Tilley (Last edit 13/10/2017)
/// </remarks>
public class onClickItem : MonoBehaviour {

	///The button the script will add a listner to.
	private Button self;
	///Corrisponding row position in the inventory
	public int row;
	///Corrisponding column position in the inventory
	public int col;
	///Inventory used to call Inventory.itemClicked() 
	public Inventory parent;

	/// <summary>
	/// Initialises the script setting self to the button component and adding a listener for the button
	/// to call pressed() .
	/// </summary>
	void Start () {
		self = this.GetComponent<Button>();
		self.onClick.AddListener (pressed);
	}

	/// <summary>
	/// Called when the button is pressed.
	/// Calls Inventory.itemClicked() with row and col as the parameters 
	/// </summary>
	void pressed(){
		Debug.Log ("Button Pressed row:" + row + " col: " + col);
		parent.itemClicked(row,col);
	}


}
