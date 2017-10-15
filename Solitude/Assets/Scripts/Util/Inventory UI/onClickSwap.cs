using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Used by the Inventory script to check if the player has clicked the button this script is attached to.
/// Attached to Swap button in the Options prefab within Unity.
/// </summary>
///<remarks> 
/// By Alexander Tilley (Last edit 13/10/2017)
/// </remarks>
public class onClickSwap : MonoBehaviour {

	///The Buttton component of the GameObject
	private Button self;
	///The Game Inventory to call Inventory.swap() function when clicked
	public Inventory parent;
	///Stores if the swap button has been pressed
	public bool swap = false;

	/// <summary>
	/// Initialises the script setting self to the button component and adding a listener for the button
	/// to call pressed() .
	/// </summary>
	void Start () {
		self = this.GetComponent<Button>();
		self.onClick.AddListener (pressed);
	}

	/// <summary>
	/// Toggles the swap variable between true and false
	/// </summary>
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
