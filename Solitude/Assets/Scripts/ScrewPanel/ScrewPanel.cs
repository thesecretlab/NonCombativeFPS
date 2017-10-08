using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * Screw Pannel Minigame, randomly palces screws onto a canvas for the place to click them all to complete
 * the acitvity.
 * 
 *  Created By
 * 	Modified by Alexander Tilley (Last Modefied 2/10/2017)
 */
public class ScrewPanel : Terminal, Breakable {


    public const int screws = 4;			//Maxium Number of Screws
    public int breakPercent = 100;			//Percentage chance this has to break.
	public Inventory playerInv;				//Player Inventory (Set In Unity)
	private int canvasWid = 1920; 			//TODO Replace me with Camera.pixelwidth -10% where camera = maincamera
	private int canvasHig = 1080;			//TODO Replace me with Camera.pixelheight-10% were camera = maincamera
	public const int iconPixelSize = 295;	//Size of the Screw Icon (pressumably symetrical)

	private int[,] buttonCoords = new int[screws, 2];	//Position for screws

    private int screwed = 0;
    private bool isFixing = false;

    public override void interact() {
        if (isFixing == false) {
            screwed = 0;							//Reset Screws

			for (int i = 0; i < screws; i++) {
				buttonCoords [i,0] = Random.Range (1, (canvasWid - 60*2)/(iconPixelSize+20));		//Inside the canvas postioned in icon sized grid
				buttonCoords [i,1]= Random.Range (1, (canvasHig - 60*2)/(iconPixelSize+20));		//Inside the canvas postioned in icon sized grid
				int OneLessScrew = 1;																//Number of times to try and repostion if collsion
				for (int s = 0; s < i; s++) {
					if (buttonCoords [i, 0] == buttonCoords [s, 0] && buttonCoords [i, 1] == buttonCoords [s, 1]) {//If Collsion
						if (OneLessScrew == 0) {		//If have already tried OneLessScrew times.
                            Debug.Log("screwed is equal to" + screwed);
							buttonCoords [i,0] = -1;
							buttonCoords [i,1]= -1;
							screwed++;
                            Debug.Log("screwed is now equal to" + screwed);
                        } else {
							buttonCoords [i,0] = Random.Range (1, (canvasWid - 60*2)/(iconPixelSize+20));		//Try and replace.
							buttonCoords [i,1]= Random.Range (1, (canvasHig - 60*2)/(iconPixelSize+20));
							s = 0;
							OneLessScrew--;				
						}
					}

				}
			}

            for (int i = 0; i < screws; i++) {		//Instanite and replace screws.
                Debug.Log("Screwing : "+i);
				GameObject newscrew = Instantiate(this.ui.GetComponent<ScrewPanelControler>().screw, ui.transform);					//Instantiate a screw.
				newscrew.transform.position = new Vector3(buttonCoords[i,0]*iconPixelSize, buttonCoords[i,1]*iconPixelSize, 0);		//Put in postion
				newscrew.GetComponent<Button>().onClick.AddListener(delegate { clickButton(newscrew); });							//On Click call clickButton

                /*GameObject newscrew = Instantiate(this.ui.GetComponent<ScrewPanelControler>().screw, ui.transform);				//LEGACY CODE
                newscrew.transform.position = new Vector3(Random.Range(60, canvasWid - 60), Random.Range(60, canvasHig - 60), 0);
                newscrew.GetComponent<Button>().onClick.AddListener(delegate { clickButton(newscrew); });*/
            }
            isFixing = true;
        }
        show();
    }

    public void onBreak() {
        screwed = 0;
        this.setActive(true);
    }

    protected override void initialise() {
        new BreakEvent(this, breakPercent);
        this.onBreak();
    }

	public void clickButton(GameObject button) {
		button.gameObject.SetActive(false);
        if((screwed += 1) == screws) {
            onFix();
        }
    }

    public void onFix() {
		if (screwed >= screws) {
			if (playerInv.consume (Inventory.POWERCABLE_ID, 1, 0, 0)) {
				hide();
				isFixing = false;
				this.setActive(false);	
			}
		}
    }

    protected override void doUpdate() {
        
    }
    protected override void onClose() {

    }
}
