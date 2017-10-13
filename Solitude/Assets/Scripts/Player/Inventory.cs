using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Item contains the type of item and how many are stacked.
/// It Also includes the GameObjects displaying the sprite and ammount that are stacked.
/// </summary>
public struct item{
	///integear ID corrisponding to the item type
	public int id { get; set; }
	///Number of the item stacked
	public int ammount { get; set; }
	///The GameObject displaying the sprite for the specific item type.
	public GameObject obj {get; set;}
	///The GameObject displaying the text indicating how many are stacked.
	public GameObject text {get; set;}
}



//code for item switching adapted from https://www.youtube.com/watch?v=Dn_BUIVdAPg&ab_channel=Brackeys
//items should be made a child of the itemholder object on the player for this to work

/// <summary>
/// Inventory Script handles player item storage and selection. Items are stored as a struct in a 2D array matching rows and cols
/// as they are displayed in game. The item sturct contains "id" representing the object type, "ammount" the ammount of the object 
/// and "obj" the object used to display the image of the icon. 
/// The icons (sprites) are stored in "pictures" array with position corrisponding to "id". 
/// </summary>
/// <remarks> 
/// By Brendan
/// Modified for inventory storage by Alexander Tilley (Last edit 04/10/2017)
/// </remarks>
public class Inventory : MonoBehaviour {

	///Current selected position in hotbar.
	public int selectedItem = 0;
	///Main Camera to get resolution (pre-Set in Unity).
	public Camera mainCam;
	///Item Options to perform opertions (Pre-set in Unity).
	public GameObject options;
	///Toaster for giving player feedback (Pre-Set in Unity).
	public Toast toaster;

	///First Selected Item row position for operations.
	public int sel_row_1;
	///First Selected Item column position for operations.
	public int sel_col_1;
	///Second Selected Item row position for operations.
	public int sel_row_2;
	///Second Selected Item column position for operations.
	public int sel_col_2;


	///The maximum number all items can stack to.
	private const int MAXSTACK = 20;
	///Total number of rows in the inventory.
	private const int INV_ROWS = 5;
	///Total number of columns in the inventory.
	private const int INV_COLUMNS = 4;

	///The Prefabricated Object for displaying the item picture (pre-set in Unity).
	public GameObject emptyImage;
	///The Prefabricated Object for displaying the ammount of stacked items (pre-set in Unity).
	public GameObject emptyText;


	///Inventory used to store items for the player.
	public item [,] inventory = new item[INV_ROWS,INV_COLUMNS];

	///Unqiue identifier for an empty inventory slot.
	public const int EMPTY_ID = 			0;
	///Unqiue identifier for a Screw driver.
	public const int SCREWDRIVER_ID = 		1;
	///Unqiue identifier for a Plasma cutter..
	public const int PLASMACUTTER_ID = 		2;
	///Unqiue identifier for a Welder.
	public const int WELDER_ID = 			3;
	///Unqiue identifier for a  Power cable.
	public const int POWERCABLE_ID = 		4;
	///Unqiue identifier for a Circuit board.
	public const int CIRCUITBOARD_ID = 		5;
	///Unqiue identifier for Scrap metal.
	public const int SCRAPMETAL_ID = 		6;
	///Unqiue identifier for Screws.
	public const int SCREWS_ID = 			7;


	///The status of whether or not the main inventory is displayed.
	public bool displayed = false;

	///Array of sprites corisponding to item ids (pictures pre-set in Unity).
	public Sprite[] pictures = new Sprite[8];
	///Array of Gameobjects corisponding to item ids for hand held objects.
	public GameObject[] heldItem = new GameObject[8];
	///Array of Strings corrisoinding to item ids for item names.
	public string[] itemNames = new string[8];

	///The height of the screen in pixels
	public float HEIGHT = 0;
	///The height of the screen in pixels
	public float WIDTH = 0;

	//public Vector3 NextPos = new Vector3(0.0f+(0.01f*WIDTH),0.0f+(HEIGHT-10.0f),1.0f);




	/// <summary>
	/// Initialises the Inventory by positioning each Item using the screen size. As well as positioning the Options GameObject.
	/// It also adds the item name's to itemNames and calls giveRandomItem(1,3) six times.
	/// It also sets the values for each inventory item's onClickItem.cs to match the row and column it is for operation calls.
	/// </summary>
	void Start () {
		try{//Allows button click comminication to this script
			options.GetComponentsInChildren<onClickSwap> () [0].parent = this;	//Link Swap Button		
			options.GetComponentsInChildren<onClickDrop> () [0].parent = this;	//Link Drop Button
		}catch{
			Debug.Log("WARNING ITEM OPTIONS IS BROKEN");
		}
		//------------------------------------------------------------------------------------ Inventory Display Setup
		WIDTH = mainCam.pixelWidth;							//Gets Camera width and height for HUD display
		HEIGHT = mainCam.pixelHeight;
		options.SetActive(false);							//Disable Options

		int hotBarOffSet = 0;
		item blank = new item();							//Empty object to intialise

		//Debug.Log ("Screen Width: " + WIDTH + " Height: " + HEIGHT);

		for (int r = 0; r < INV_ROWS; r++) {				//For all rows in inventory
			for (int c = 0; c < INV_COLUMNS; c++) {			//For all cols in inventory
				if (r == 1) {
					hotBarOffSet = 90;
				}
				blank.ammount = 0;
				blank.id = 0;
				blank.obj = Instantiate(emptyImage);
				blank.text = Instantiate (emptyText);
				blank.text.GetComponent<onClickItem> ().parent = this;
				blank.text.GetComponent<onClickItem> ().row = r;
				blank.text.GetComponent<onClickItem> ().col = c;
				inventory[r,c]=blank;																		//Set to empty

				inventory [r, c].obj.transform.SetParent (this.gameObject.transform);						//Position within Parent
				inventory [r, c].obj.GetComponent<RectTransform> ().localScale = Vector3.one+(Vector3.one/2);				//Set Scale to normal	
				inventory [r, c].obj.GetComponent<RectTransform> ().localPosition = Vector3.one;			//Centre Postion
				inventory [r, c].obj.GetComponent<RectTransform> ().localPosition = 
					newVector((c*150)+0.0f-(WIDTH*0.45f),(hotBarOffSet+r*150)+0.0f-(HEIGHT*0.40f),5.0f);	//Position In Table form

				inventory [r, c].text.transform.SetParent (this.gameObject.transform);						//Position within Parent
				inventory [r, c].text.GetComponent<RectTransform> ().localScale = Vector3.one+(Vector3.one/2);				//Set Scale to normal	
				inventory [r, c].text.GetComponent<RectTransform> ().localPosition = Vector3.one;			//Centre Postion
				inventory [r, c].text.GetComponent<RectTransform> ().localPosition = 
					newVector((c*150)+0.0f-(WIDTH*0.45f),(hotBarOffSet+r*150)+0.0f-(HEIGHT*0.40f),5.0f);	//Position In Table form
				updateText(r,c);

				//inventory [r, c].obj.GetComponentsInChildren<Text> () [0].text = "";
			}
		}
		toggleInventory ();		//Closes the main inventory by defualt

		//options.transform.SetParent(this.gameObject.transform);
		options.transform.localPosition = Vector3.one;			//Centre Postion
		//Debug.Log ("Mouse Pos "+Input.mousePosition);
		options.transform.localPosition = newVector((((INV_COLUMNS-1)/2.0f)*150)+0.0f-(WIDTH*0.45f),(hotBarOffSet+INV_ROWS*150-50)+0.0f-(HEIGHT*0.40f),5.0f);

		itemNames [0] = "Empty";
		itemNames [1] = "Screw Driver";
		itemNames [2] = "Plasma Cutter";
		itemNames [3] = "Welder";
		itemNames [4] = "Power Cable";
		itemNames [5] = "Circuit Board";
		itemNames [6] = "Scrap Metal";
		itemNames [7] = "Screws";

		//TODO Set Up GameObject Like in video in discrption for hand held items setActive false for all

		SelectItem(INV_COLUMNS-1);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
	}




	/// <summary>
	/// Update is called once per frame to check for input to change the selected hotbar item as well as
	/// open the main inventory if 'Q' is pressed.
	/// </summary>
	void Update() {

		int prevSelectedItem = selectedItem;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			selectedItem = (selectedItem + 1) % INV_COLUMNS;

			/*if (selectedItem >= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem++;*/
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (selectedItem - 1 >= 0) {
				selectedItem--;
			}else{
				selectedItem = INV_COLUMNS-1;
			}
			/*if (selectedItem <= 0)
                selectedItem = transform.childCount - 1;
            else
                selectedItem--;*/
		}

		//can be expanded up to 9 if needed by copy-pasting the second statement and incrementing values
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedItem = 0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			selectedItem = 1;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
		{
			selectedItem = 2;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
		{
			selectedItem = 3;
		}

		if (prevSelectedItem != selectedItem)
		{
			SelectItem(prevSelectedItem);
		}

		if (Input.GetKeyDown(KeyCode.Q)) {				//Player Opens/Closes Inventory
			if (displayed == true) {
				displayed = false;			//If Open: Close
				Player.playerObj.FPSEnable(true);
				options.SetActive (false);		//Hide options
			} else {
				displayed = true;			//If Closed: Open
				Player.playerObj.FPSEnable(false);

			}
			toggleInventory ();		//Updates to display status
		}

	}

	/// <summary>
	/// Changes the player's held item.
	/// </summary>
	/// <param name="prevItem"> prevItem is the previous held item which SelectItem() will hide</param>
	void SelectItem(int prevItem)
	{
		//Debug.Log ("Selected Item: "+selectedItem + "Prev Item: "+prevItem);
		//int i = 0;
		//TODO make sure heldItem is filled in Unity

		if (heldItem [inventory [0, selectedItem].id] != null) {		//Therotically Works
			heldItem[inventory[0,selectedItem].id].SetActive(true);		//Display this held item

			if (heldItem [inventory [0, prevItem].id] != null) {
				heldItem[inventory[0,prevItem].id].SetActive(false);
			}
		}

		/*foreach (Transform item in transform) DO NOT USE THIS
        {
            if (i == selectedItem)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            i++;
        }*/


	}

	/// <summary>
	/// Toggles wherther the main inventory is displayed or not.
	/// </summary>
	public void toggleInventory(){
		for (int r = 1; r < INV_ROWS; r++) {
			for (int c = 0; c < INV_COLUMNS; c++) {		//For Everything but the hotbar
				inventory[r,c].obj.SetActive(displayed);//SetActive True or False
				inventory[r,c].text.SetActive(displayed);//SetActive True or False
			}
		}
	}

	/// <summary>
	/// Gives a random item to the player and calls insertAtTop() to add it to the inventory
	/// </summary>
	/// <returns><c>true</c>, if the random item was given, <c>false</c> otherwise.</returns>
	/// <param name="min">The Minimum ammount of the item to give the player</param>
	/// <param name="max">The Maximum ammount of the item to give the player</param>
	public bool giveRandomItem(int min,int max){
		if (max > MAXSTACK || max <= 0) {			//If max is valid
			max = MAXSTACK;
		}
		if (min <= 0) {						//If min is valid
			min = 1;
		} else if (min > MAXSTACK) {
			min = MAXSTACK;
		}
		return insertAtTop (Random.Range (4, 7), Random.Range (min, max));	//Insert random item of random ammount
	}
		
	/// <summary>
	/// Typicalled called from onClickItem.cs to set the selection varible positions (e.g. sel_row_1) for operations to be applied.
	/// Also calls swap() if operation has been pressed by the user by checking options.OnClickSwap().swap.
	/// </summary>
	/// <param name="row">The Row position of the item.</param>
	/// <param name="col">The Column position of the item.</param>
	public void itemClicked(int row,int col){
		//Debug.Log ("Activated  row:" + row + " col: " + col);

		if (options.activeSelf) {			//Options Already Displaued
			sel_row_2 = row;				//Get Second Selection
			sel_col_2 = col;
			if (options.GetComponentsInChildren<onClickSwap> () [0].swap) {
				swap (sel_row_1, sel_col_1, sel_row_2, sel_col_2);
			}
			options.SetActive (false);		//Hide options
		} else if(inventory[row,col].id != EMPTY_ID) {
			options.SetActive (true);		//Display Options
			sel_row_1 = row;				//First Selection
			sel_col_1 = col;
			//Debug.Log ("Mouse Pos "+Input.mousePosition);
			//TODO set postion of options
		}

	}
		
	/// <summary>
	/// Swaps the specified item at inventory[row, col] with the item at inventory[r,c].
	/// </summary>
	/// <param name="row">Row of the specified item.</param>
	/// <param name="col">Column of the specified item.</param>
	/// <param name="r">Row of the specified item to swap to</param>
	/// <param name="c">Column of the specified item to swap to.</param>
	public void swap(int row,int col,int r, int c){
		int holdAmmount = inventory[r,c].ammount;		//Holds item ammount
		int holdId = inventory [r, c].id;				//Holdes item id

		inventory [r, c].id = inventory [row, col].id;											//Copy item row col into r c
		inventory [r, c].ammount = inventory [row, col].ammount;
		inventory [r, c].obj.GetComponent<Image> ().sprite = pictures [inventory [r, c].id];
		updateText(r,c);

		inventory [row, col].id = holdId;															//Copy item hold into row col
		inventory [row, col].ammount = holdAmmount;
		inventory [row, col].obj.GetComponent<Image> ().sprite = pictures [inventory [row, col].id];
		updateText(row,col);

	}

	/// <summary>
	/// Discards the specified item at inventory[row,col] to drop in the gameworld.
	/// </summary>
	/// <param name="row">Row of the specified item.</param>
	/// <param name="col">Column of the specified item.</param>
	public void drop(int row, int col){
		if (row <= -1 || col <= -1) {						//If No value default to selected postion
			row = sel_row_1;
			col = sel_col_1;
		}
		if (inventory [row, col].ammount > 1) {				//Drop one of item
			inventory [row, col].ammount--;
		}else if(inventory [row, col].ammount <= 1){		//If it will be empty change to empty
			inventory [row, col].id = EMPTY_ID;
			inventory [row, col].ammount = 0;
			inventory [row, col].obj.GetComponent<Image> ().sprite = pictures [EMPTY_ID];
		}
		//TODO Drop physical object
		updateText(row,col);
	}

	/// <summary>
	/// Uses one of the specified item at inventory[row,col].
	/// </summary>
	/// /// <returns><c>true</c>, if the item was used, <c>false</c> otherwise.</returns>
	/// <param name="row">Row of the specified item.</param>
	/// <param name="col">Column of the specified item..</param>
	public bool use(int row, int col){
		if (row <= -1 || col <= -1 || row > INV_ROWS || col > INV_COLUMNS) { //If Invalid range return false
			return false;
		}
		if (inventory [row, col].ammount > 1) {				//Drop one of item
			inventory [row, col].ammount--;
		}else if(inventory [row, col].ammount <= 1){		//If it will be empty change to empty
			inventory [row, col].id = EMPTY_ID;
			inventory [row, col].ammount = 0;
			inventory [row, col].obj.GetComponent<Image> ().sprite = pictures [EMPTY_ID];
		}
		updateText(row,col);
		return true;
	}

	//Consumes the AMMOUNT of type ITEMID and returns true and only consume if it did. 
	//(startRow and StartCol is for recusive calls should be 0 for normal calls )
	/// <summary>
	/// Consumes the ammount of items of type ItemID, Searches bottom to top for the items.
	/// </summary>
	/// /// <returns><c>true</c>, if the ammount of items exactly were consumsed, <c>false</c> otherwise.</returns>
	/// <param name="ItemID">Type of item (e.g Inventory.POWERCABLE_ID)</param>
	/// <param name="ammount">Ammount to consume</param>
	/// <param name="startRow">Start row used for recursive calls otherwise should be 0.</param>
	/// <param name="StartCol">Start columns used for recursive calls otherwise should be 0.</param>
	public bool consume(int ItemID,int ammount,int startRow,int StartCol){
		int rRow = -1;
		int rCol = -1;
		if (findNextItem (startRow, StartCol, ItemID, out rRow, out rCol)) {									//Does ITEMID exists
			if (inventory [rRow, rCol].ammount >= ammount) {											//Is there enough
				if (ammount - inventory [rRow, rCol].ammount == 0) {									//Ist it exactly enough
					inventory [rRow, rCol].id = EMPTY_ID;
					inventory [rRow, rCol].ammount = 0;
					inventory [rRow, rCol].obj.GetComponent<Image> ().sprite = pictures [EMPTY_ID];
					updateText (rRow, rCol);
					toaster.addText ("Used  " + ammount + " " + itemNames [ItemID],3.0f);
					return true;
				} else {
					inventory [rRow, rCol].ammount = inventory [rRow, rCol].ammount-ammount;			//Left Overs
					updateText (rRow, rCol);
					toaster.addText ("Used  " + ammount + " " + itemNames [ItemID],3.0f);
					return true;	
				}
			} else {
				if (consume (ItemID, ammount - inventory [rRow, rCol].ammount, rRow + 1, rCol + 1)) {	//If there isnt enough is there another stack I can consume from
					inventory [rRow, rCol].id = EMPTY_ID;
					inventory [rRow, rCol].ammount = 0;
					inventory [rRow, rCol].obj.GetComponent<Image> ().sprite = pictures [EMPTY_ID];
					updateText (rRow, rCol);
					return true;
				}
			}
		}
		toaster.addText ("You Need  "+ammount+" "+ itemNames [ItemID]+" to complete this task",3.0f);
		return false;
	}
		
	/// <summary>
	/// Finds the next item inventory from bottom to top that matches item_id.
	/// </summary>
	/// <returns><c>true</c>, if next item was found, <c>false</c> otherwise.</returns>
	/// <param name="row">Start row used for recursive calls otherwise should be 0.</param>
	/// <param name="col">Start columns used for recursive calls otherwise should be 0.</param>
	/// <param name="item_id">Item identifier (e.g Inventory.POWERCABLE_ID).</param>
	/// <param name="fRow">The varible to output the Row position of an item of type item_id</param>
	/// <param name="fCol">The varible to output the Colunm position of an item of type item_id</param>
	bool findNextItem(int row,int col,int item_id, out int fRow,out int fCol){
		for (int r = row; r < INV_ROWS; r++) {					//For all rows in inventory from row
			for (int c = col; c < INV_COLUMNS; c++) {			//For all cols in inventory from col
				if (inventory[r,c].id == item_id) {			//Where item.id == item_id
					fRow = r;
					fCol = c;
					return true;								//Return True and array cords.
				}
			}
		}
		fRow = -1;
		fCol = -1;
		return false;
	}

	/// <summary>
	/// Moves all items in the inventory exluding the hotbar to the top of the inventory.
	/// </summary>
	void floatItems(){
		int row = 1;		//Skip Hotbar to look for objects
		int col = 0;		//Records Objects bottom up
		bool floated = false;

		for (int r = INV_ROWS-1; r >=1; r--) {										//For all rows in inventory top to bottom
			for (int c = INV_COLUMNS-1; c >=0; c--) {								//For all cols in inventory top to bottom
				if (inventory[r,c].id == EMPTY_ID) {								//Where item.id == EMPTY
					for (row = row; row < INV_ROWS && floated == false; row++) {				//For all rows in inventory from bottom to top
						for (col = col; col < INV_COLUMNS && floated == false; col++) {		//for all columns in inventory from bottom to top
							if (row >= r && (col >= c || row > r)) {						//If Crossed Search Over
								floated = true;
								r = 0;
								c = 0;
							} else if(inventory[row,col].id != EMPTY_ID) {					//Where full swap with empty
								inventory [r,c].id = inventory [row,col].id;
								inventory [r,c].ammount = inventory [row,col].ammount;
								inventory [r, c].obj.GetComponent<Image> ().sprite = pictures [inventory [row,col].id];
								inventory [row,col].id = EMPTY_ID;
								inventory [row,col].ammount = 0;
								inventory [row, col].obj.GetComponent<Image> ().sprite = pictures [EMPTY_ID];
								floated = true;
							}

						}
					}
					floated = false;
				}
			}
		}
	}

	//Insert an item to the top most empty slot
	/// <summary>
	/// Inserts a number of a specifc item into the highest avalible inventory slot.
	/// </summary>
	/// <returns><c>true</c>, if the item was inserted, <c>false</c> otherwise.</returns>
	/// <param name="id">Item Identifier (e.g Inventory.POWERCABLE_ID).</param>
	/// <param name="ammount">Ammount of the item to insert.</param>
	bool insertAtTop(int id, int ammount){
		int row = -1;
		int col = -1;
		if (findNextItem (0, 0, id,out row, out col)) {										//If Object Exists
			inventory [row, col].ammount = inventory [row, col].ammount + ammount;		//Add to stack
			toaster.addText ("You Picked Up  " + ammount + " " + itemNames [id],3.0f);
			updateText(row,col);
			if (inventory [row, col].ammount > MAXSTACK) {								//If Larger Than Max Stack
				ammount = inventory [row, col].ammount - MAXSTACK;						//Ammount = diffrence
			} else {
				id = 0;																	//If Not Larger than Max Stack do not insert new object
			}
		}
		if (id != 0) {
			for (int r = INV_ROWS - 1; r >= 1; r--) {										//For all rows in inventory top to bottom
				for (int c = INV_COLUMNS - 1; c >= 0; c--) {								//For all cols in inventory top to bottom
					if (inventory [r,c].id == EMPTY_ID) {									//Where slot is empty
						inventory[r,c].id = id;											//Insert
						inventory[r,c].ammount = ammount;
						inventory [r, c].obj.GetComponent<Image> ().sprite = pictures [id];
						toaster.addText ("You Picked Up  " + ammount + " " + itemNames [id],3.0f);
						updateText(r,c);
						return true;
					}
				}
			}	
		}
		return false;
		Debug.Log ("Inventory Full Or Max Stack Achieved");	//TODO display message to player
	}

	/// <summary>
	/// Updates the ammount displayed of an item typically called after an inventory operation.
	/// </summary>
	/// <param name="row">Row of the specified item.</param>
	/// <param name="col">Column of the specified item.</param>
	public void updateText(int row,int col){
		if (inventory [row, col].id == 0) {
			inventory [row, col].text.GetComponent<Text> ().text = "";										//If there is no ammount
		} else if (inventory [row, col].ammount <= 9) {
			inventory [row, col].text.GetComponent<Text> ().text = "x0" + inventory [row, col].ammount;		//If there is less than 10
		} else {
			inventory [row, col].text.GetComponent<Text> ().text = "x" + inventory [row, col].ammount;		//If there is more than 9
		}
		//TODO Could also update item image in this function
	}
		
	/// <summary>
	/// Creates a newVector
	/// </summary>
	/// <returns>The vector.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private Vector3 newVector(float x, float y,float z){
		Vector3 toReturn = new Vector3 (x, y, z);
		return toReturn;
	}

}
