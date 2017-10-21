using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Screw Pannel Minigame, randomly places screws onto a canvas for the player to click them all and 
/// use Inventory.POWERCABLE_ID to complete the activity.
/// </summary>
/// <remarks> 
/// Created By Jeffery Albion
/// Modified By Alexander Tilley to prevent screw overlaps (Last edit 13/10/2017)
/// </remarks>
public class ScrewPanel : Terminal, Breakable {

	///Maxium Number of Screws
    public const int screws = 4;	
	///Percentage chance this has to break.	
    public int breakPercent = 100;	
	///Player Inventory (Set In Unity)	
	public Inventory playerInv;				
	private int canvasWid = 1920; 			//TODO Replace me with Camera.pixelwidth -10% where camera = maincamera
	private int canvasHig = 1080;			//TODO Replace me with Camera.pixelheight-10% were camera = maincamera
	///Size of the Screw Icon (pressumably symetrical)
	public const int iconPixelSize = 295;	

	///Position for screws
	private int[,] buttonCoords = new int[screws, 2];	

	///varable to stored the screws that have been screwed.
    private int screwed = 0;
	///boolean  to store if the panel isFixing or not.
    private bool isFixing = false;

	
	///when a screw is interacted with.
    public override void interact() {
        
		
		
		if (isFixing == false) {
            
			//Reset Screws
			screwed = 0;							

			
			for (int i = 0; i < screws; i++) {
				//Inside the canvas postioned in icon sized grid
				buttonCoords [i,0] = Random.Range (1, (canvasWid - 60*2)/(iconPixelSize+20));	
				//Inside the canvas postioned in icon sized grid				
				buttonCoords [i,1]= Random.Range (1, (canvasHig - 60*2)/(iconPixelSize+20));		
				//Number of times to try and repostion if collsion
				int OneLessScrew = 1;
				
				for (int s = 0; s < i; s++) {
					//If Collsion
					if (buttonCoords [i, 0] == buttonCoords [s, 0] && buttonCoords [i, 1] == buttonCoords [s, 1]) {
						//If have already tried OneLessScrew times.
						if (OneLessScrew == 0) {		
                            Debug.Log("screwed is equal to" + screwed);
							buttonCoords [i,0] = -1;
							buttonCoords [i,1]= -1;
							screwed++;
                            Debug.Log("screwed is now equal to" + screwed);
                        } else {
							//Try and replace.
							buttonCoords [i,0] = Random.Range (1, (canvasWid - 60*2)/(iconPixelSize+20));		
							buttonCoords [i,1]= Random.Range (1, (canvasHig - 60*2)/(iconPixelSize+20));
							s = 0;
							OneLessScrew--;				
						}
					}

				}
			}
			//Instanite and replace screws.
            for (int i = 0; i < screws; i++) {		
                Debug.Log("Screwing : "+i);
				//Instantiate a screw.
				GameObject newscrew = Instantiate(this.ui.GetComponent<ScrewPanelControler>().screw, ui.transform);	
				//Put in postion				
				newscrew.transform.position = new Vector3(buttonCoords[i,0]*iconPixelSize, buttonCoords[i,1]*iconPixelSize, 0);		
				//On Click call clickButton
				newscrew.GetComponent<Button>().onClick.AddListener(delegate { clickButton(newscrew); });							

                /*GameObject newscrew = Instantiate(this.ui.GetComponent<ScrewPanelControler>().screw, ui.transform);				//LEGACY CODE
                newscrew.transform.position = new Vector3(Random.Range(60, canvasWid - 60), Random.Range(60, canvasHig - 60), 0);
                newscrew.GetComponent<Button>().onClick.AddListener(delegate { clickButton(newscrew); });*/
            }
            isFixing = true;
        }
        show();
    }
	
	///when the screw panel breaks set the screws screwed to 0 and sets active to be true.
    public void onBreak() {
        screwed = 0;
        this.setActive(true);
    }
	///initialise break event.
    protected override void initialise() {
        new BreakEvent(this, breakPercent);
        this.onBreak();
    }

	///activated when screw is clicked.
	public void clickButton(GameObject button) {
		button.gameObject.SetActive(false);
        if((screwed += 1) == screws) {
            onFix();
        }
    }

	///when all screws are screwed and the player has spare Inventory.POWERCABLE_ID the crew pannel will be fixed.
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
