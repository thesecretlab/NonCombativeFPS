using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct item{
	public int id { get; set; }		//Object Type
	public int ammount { get; set; }//Ammount of object or left uses of obbject;
}

public class ItemSwitching : MonoBehaviour {

    //code adapted from https://www.youtube.com/watch?v=Dn_BUIVdAPg&ab_channel=Brackeys
    //items should be made a child of the itemholder object on the player for this to work
	public GameObject row;
	public GameObject[] display;
	public Image[] items;
	//public GameObject[] items;
	public int xcords;
	public int ycords;
	public int zcords;

    public int selectedItem = 0;

	private const int MAXSTACK = 20;				//Max number of stackable items
	private const int INV_ROWS = 5;					//Inventory Rows
	private const int INV_COLUMNS = 4;				//Inventory columns

	//public struct item items[][];
	public GameObject emptyImage;
	public GameObject [,] visible = new GameObject[INV_ROWS,INV_COLUMNS];

	public Sprite SCREWDRIVER_PIC;
	public Sprite PLASMACUTTER_PIC;
	public Sprite WELDER_PIC;





	public item [,] inventory = new item[INV_ROWS,INV_COLUMNS];	//2D array of items

	public const int EMPTYID = 0;					//Empty Inventory Slot ID

	public const int SCREWDRIVER = 	1;				//ScrewDriver ID
	public const int PLASMACUTTER = 	2;			//PlasmaCutter ID
	public const int WELDER = 			3;			//Welder ID
	public const int POWERCABLE = 		4;			//PowerCable ID
	public const int CIRCUITBOARD = 	5;			//Circuit Board ID
	public const int SCRAPMETAL = 		6;			//Scrap Metal ID
	public const int SCREWS = 			7;			//Screws ID
	public float HEIGHT =  Screen.height;		//Window Height
	public float WIDTH = Screen.width;			//Window Width
	//public Vector3 NextPos = new Vector3(0.0f+(0.01f*WIDTH),0.0f+(HEIGHT-10.0f),1.0f);



    // Use this for initialization
    void Start () {
		int hotBarOffSet = 0;
		Debug.Log ("Screen Width: " + WIDTH + " Height: " + HEIGHT);
        SelectItem();
		item blank;
		blank.ammount = 0;
		blank.id = 0;
		for (int r = 0; r < INV_ROWS; r++) {				//For all rows in inventory
			for (int c = 0; c < INV_COLUMNS; c++) {			//For all cols in inventory
				if (r == 1) {
					hotBarOffSet = 200;
				}
				inventory[r,c]=blank;						//Set to empty
				visible[r,c]= Instantiate(emptyImage);
				visible [r, c].transform.SetParent (this.gameObject.transform);
				visible [r, c].GetComponent<RectTransform> ().localScale = Vector3.one;
				visible [r, c].GetComponent<RectTransform> ().localPosition = Vector3.one;
				visible [r, c].GetComponent<RectTransform> ().localPosition = 
					newVector((c*100)+0.0f-(WIDTH*0.45f),(hotBarOffSet+r*100)+0.0f-(HEIGHT*0.40f),5.0f);			
				//visible[r,c].GetComponent<RectTransform> ().position.Set (1.0f, 1.0f, 1.0f);
				//visible [r, c].transform.localPosition.Set (1.0f, 1.0f, 1.0f);
			}
		}
		visible [0, 0].transform.localPosition.Set (10.0f, 10.0f, 0.0f);
		visible [0, 0].GetComponent<Image>().sprite = SCREWDRIVER_PIC;
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
				if (inventory[r,c].id == EMPTYID) {								//Where item.id == EMPTY
					for (row = row; row < INV_ROWS && floated == false; row++) {				//For all rows in inventory from bottom to top
						for (col = col; col < INV_COLUMNS && floated == false; col++) {		//for all columns in inventory from bottom to top
							if (row >= r && (col >= c || row > r)) {						//If Crossed Search Over
								floated = true;
								r = 0;
								c = 0;
							} else if(inventory[row,col].id != EMPTYID) {					//Where full swap with empty
								inventory [r,c].id = inventory [row,col].id;
								inventory [r,c].ammount = inventory [row,col].ammount;
								inventory [row,col].id = EMPTYID;
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
				if (inventory [r,c].id == EMPTYID) {									//Where slot is empty
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
