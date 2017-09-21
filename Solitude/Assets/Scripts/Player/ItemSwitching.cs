using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct item{
	public int id { get; set; }			//Object Type
	public int ammount { get; set; }	//Ammount of object or left uses of obbject;
	public GameObject obj {get; set;}	//Sprite Image
	public GameObject text {get; set;}	//Text to display ammount
}



//code for item switching adapted from https://www.youtube.com/watch?v=Dn_BUIVdAPg&ab_channel=Brackeys
//items should be made a child of the itemholder object on the player for this to work

/*
 * Inventory Script handles player item storage and selection. Items are stored as a struct in a 2D array matching rows and cols
 * as they are displayed in game. The item sturct contains "id" representing the object type, "ammount" the ammount of the object
 * and "obj" the object used to display the image of the icon. 
 * The icons (sprites) are stored in "pictures" array with position corrisponding to "id". 
 * 
 * By Brendan
 * Modified for inventory storage by Alexander Tilley 21/09/2017
*/
public class ItemSwitching : MonoBehaviour {

	public int selectedItem = 0;					//

	public Camera mainCam;							//Main Camera to make sure inventory fits screen (pre-Set Unity)
	public GameObject options;						//Item Options to perform opertions (pre-set Unity)

	public int sel_row_1;							//Selected Items rows and cols (To Hold)
	public int sel_col_1;
	public int sel_row_2;
	public int sel_col_2;



	private const int MAXSTACK = 20;				//Max number of stackable items
	private const int INV_ROWS = 5;					//Inventory Rows
	private const int INV_COLUMNS = 4;				//Inventory columns

	public GameObject emptyImage;					//Base Object Spirte (pre-set in Unity)
	public GameObject emptyText;					//Base Object text (pre-set in Unity)

	public item [,] inventory = new item[INV_ROWS,INV_COLUMNS];	//2D array of items

	public const int EMPTY_ID = 			0;			//Empty Inventory Slot ID

	public const int SCREWDRIVER_ID = 		1;			//ScrewDriver ID
	public const int PLASMACUTTER_ID = 		2;			//PlasmaCutter ID
	public const int WELDER_ID = 			3;			//Welder ID
	public const int POWERCABLE_ID = 		4;			//PowerCable ID
	public const int CIRCUITBOARD_ID = 		5;			//Circuit Board ID
	public const int SCRAPMETAL_ID = 		6;			//Scrap Metal ID
	public const int SCREWS_ID = 			7;			//Screws ID

	public bool displayed = false;						//Current display status of the main inventory (does not update)

	public Sprite[] pictures = new Sprite[8];			//Array of sprites corisponding to item ids (pics pre-set in Unity)
	public GameObject[] heldItem = new GameObject[8];	//Array of Gameobjects corisponding to item ids for hand held objects

	public float HEIGHT =  Screen.height;		//Window Height
	public float WIDTH = Screen.width;			//Window Width

	//public Vector3 NextPos = new Vector3(0.0f+(0.01f*WIDTH),0.0f+(HEIGHT-10.0f),1.0f);



	// Use this for initialization
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

		Debug.Log ("Screen Width: " + WIDTH + " Height: " + HEIGHT);

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

		//TODO Set Up GameObject Like in video in discrption for hand held items setActive false for all

		SelectItem(INV_COLUMNS-1);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
		giveRandomItem (1, 3);
	}




	// Update is called once per frame
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

	//Displays Held Item on Game Screen
	void SelectItem(int prevItem)
	{
		Debug.Log ("Selected Item: "+selectedItem + "Prev Item: "+prevItem);
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

	//changes if the main inventory is displayed or not
	public void toggleInventory(){
		for (int r = 1; r < INV_ROWS; r++) {
			for (int c = 0; c < INV_COLUMNS; c++) {		//For Everything but the hotbar
				inventory[r,c].obj.SetActive(displayed);//SetActive True or False
				inventory[r,c].text.SetActive(displayed);//SetActive True or False
			}
		}
	}

	//Gives any random possible item in ammounts between min and max
	public void giveRandomItem(int min,int max){
		if (max > MAXSTACK || max <= 0) {			//If max is valid
			max = MAXSTACK;
		}
		if (min <= 0) {						//If min is valid
			min = 1;
		} else if (min > MAXSTACK) {
			min = MAXSTACK;
		}
		insertAtTop (Random.Range (4, 7), Random.Range (min, max));	//Insert random item of random ammount
	}

	//Displays Options when an inventory item is selected and executes Swap() if needed
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

	//Swaps an items postion in the array with anothers
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

	//Drops one item
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
		updateText(row,col);
	}


	/*returns true/false if item is found and puts the lococation of the item in row and col
	 Starts from row, col, ensure intialisation and allows it to find multiple of the same object*/
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

	//Pushes all items to top of inventory but those in hotbar
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
	bool insertAtTop(int id, int ammount){
		int row = -1;
		int col = -1;
		if (findNextItem (0, 0, id,out row, out col)) {										//If Object Exists
			inventory [row, col].ammount = inventory [row, col].ammount + ammount;		//Add to stack
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
						updateText(r,c);
						return true;
					}
				}
			}	
		}
		return false;
		Debug.Log ("Inventory Full Or Max Stack Achieved");	//TODO display message to player
	}

	//Updates the displayed text of a specific item //TODO Could also update item image in this function
	public void updateText(int row,int col){
		if (inventory [row, col].id == 0) {
			inventory [row, col].text.GetComponent<Text> ().text = "";										//If there is no ammount
		} else if (inventory [row, col].ammount <= 9) {
			inventory [row, col].text.GetComponent<Text> ().text = "x0" + inventory [row, col].ammount;		//If there is less than 10
		} else {
			inventory [row, col].text.GetComponent<Text> ().text = "x" + inventory [row, col].ammount;		//If there is more than 9
		}
	}

	//Returns a vector as a var to satsify Unity
	Vector3 newVector(float x, float y,float z){
		Vector3 toReturn = new Vector3 (x, y, z);
		return toReturn;
	}

}
