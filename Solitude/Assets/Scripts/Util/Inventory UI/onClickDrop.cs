using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Used to call the Inventory.drop() function when the player clicks the button this script is attached to.
/// Attached to Drop button in the Options prefab within Unity.
/// </summary>
public class onClickDrop : MonoBehaviour {

	///The Buttton component of the GameObject
	private Button self;
	///The Game Inventory to call Inventory.drop() function when clicked
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
	/// Calls Inventory.drop().
	/// </summary>
	void pressed(){
		//Debug.Log ("Button Pressed Drop");
		parent.drop (-1,-1);
		//parent.swap();
	}
}
