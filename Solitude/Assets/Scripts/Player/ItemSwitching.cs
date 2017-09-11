using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct item{
	public int id { get; set; }			//Object Type
	public int ammount { get; set; }	//Ammount of object or left uses of obbject;
	public GameObject obj {get; set;}	//Sprite Image
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
 * Modified by Alexander Tilley 11/09/2017
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

	public item [,] inventory = new item[INV_ROWS,INV_COLUMNS];	//2D array of items

	public const int EMPTY_ID = 			0;			//Empty Inventory Slot ID

	public const int SCREWDRIVER_ID = 		1;			//ScrewDriver ID
	public const int PLASMACUTTER_ID = 		2;			//PlasmaCutter ID
	public const int WELDER_ID = 			3;			//Welder ID
	public const int POWERCABLE_ID = 		4;			//PowerCable ID
	public const int CIRCUITBOARD_ID = 		5;			//Circuit Board ID
	public const int SCRAPMETAL_ID = 		6;			//Scrap Metal ID
	public const int SCREWS_ID = 			7;			//Screws ID

	public Sprite[] pictures = new Sprite[8];		//Array of sprites corisponding to item ids (pics pre-set in Unity)

	public float HEIGHT =  Screen.height;		//Window Height
	public float WIDTH = Screen.width;			//Window Width

	//public Vector3 NextPos = new Vector3(0.0f+(0.01f*WIDTH),0.0f+(HEIGHT-10.0f),1.0f);



    // Use this for initialization
    void Start () {
		SelectItem();
		//------------------------------------------------------------------------------------ Inventory Display Setup
		WIDTH = mainCam.pixelWidth;
		HEIGHT = mainCam.pixelHeight;
		options.SetActive (false);

		int hotBarOffSet = 0;
		item blank = new item();							//Empty object to intialise

		Debug.Log ("Screen Width: " + WIDTH + " Height: " + HEIGHT);

		for (int r = 0; r < INV_ROWS; r++) {				//For all rows in inventory
			for (int c = 0; c < INV_COLUMNS; c++) {			//For all cols in inventory
				if (r == 1) {
					hotBarOffSet = 200;
				}
				blank.ammount = 0;
				blank.id = 0;
				blank.obj = Instantiate(emptyImage);
				blank.obj.GetComponent<onClickItem> ().parent = this;
				blank.obj.GetComponent<onClickItem> ().row = r;
				blank.obj.GetComponent<onClickItem> ().col = c;
				inventory[r,c]=blank;																		//Set to empty
				inventory [r, c].obj.transform.SetParent (this.gameObject.transform);						//Position within Parent
				inventory [r, c].obj.GetComponent<RectTransform> ().localScale = Vector3.one;				//Set Scale to normal	
				inventory [r, c].obj.GetComponent<RectTransform> ().localPosition = Vector3.one;			//Centre Postion
				inventory [r, c].obj.GetComponent<RectTransform> ().localPosition = 
					newVector((c*100)+0.0f-(WIDTH*0.45f),(hotBarOffSet+r*100)+0.0f-(HEIGHT*0.40f),5.0f);	//Position In Table form			//TODO Adjust gap sizing by screen size
			}
		}
	}




    // Update is called once per frame
    void Update() {

        int prevSelectedItem = selectedItem;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedItem >= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedItem <= 0)
                selectedItem = transform.childCount - 1;
            else
                selectedItem--;
        }

        if (prevSelectedItem != selectedItem)
        {
            SelectItem();
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



    }

    void SelectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selectedItem)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            i++;
        }

    }

	public void itemClicked(int row,int col){
		Debug.Log ("Activated  row:" + row + " col: " + col);

		if (options.activeSelf) {			//Options Already Displaued
			sel_row_2 = row;				//Get Second Selection
			sel_col_2 = col;
			//TODO swap();
			options.SetActive (false);		//Hide options
		} else {
			options.SetActive (true);		//Display Options
			sel_row_1 = row;				//First Selection
			sel_col_1 = col;
			options.transform.SetParent(this.gameObject.transform);
			options.transform.localPosition = Vector3.one;			//Centre Postion
			//Debug.Log ("Mouse Pos "+Input.mousePosition);
			options.transform.localPosition = inventory[row,col].obj.GetComponent<RectTransform>().localPosition;
			//TODO set postion of options
		}

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
								inventory [row,col].id = EMPTY_ID;
								inventory [row,col].ammount = 0;
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
	bool insertAtTop(item insert){
		for (int r = INV_ROWS - 1; r >= 1; r--) {										//For all rows in inventory top to bottom
			for (int c = INV_COLUMNS - 1; c >= 0; c--) {								//For all cols in inventory top to bottom
				if (inventory [r,c].id == EMPTY_ID) {									//Where slot is empty
					inventory[r,c] = insert;											//Insert
					return true;
				}
			}
		}
		return false;
	}

	//Returns a vector as a var to satsify Unity
	Vector3 newVector(float x, float y,float z){
		Vector3 toReturn = new Vector3 (x, y, z);
		return toReturn;
	}

}
