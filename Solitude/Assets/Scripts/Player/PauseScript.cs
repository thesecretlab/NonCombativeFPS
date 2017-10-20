using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Controls the pausing and unpausing of the game.
///Controls stopping all tasks when the game is paused.
///<summary>
///<remarks>
///Authors:
///</remarks>
public class PauseScript :MonoBehaviour, Window {
    ///track if game is paused
    ///public bool paused;
    ///used to point to the pause ui
    public GameObject pauseUI;
	///object declaration for pause script.
    static PauseScript pauseScript;
    ///used to point to the settings ui
    public GameObject pauseSettingsUI;
	///gameobject to store pausehelp ui.
    public GameObject pauseHelpUI;
	///pausesub object declarations for settings and help.
    PauseSub settings, help;
	///count
    int count = 1;
    /*public void setPause(bool pause) {
        Time.timeScale = System.Convert.ToInt32(!pause);
        //paused = pause;
        pauseUI.gameObject.SetActive(pause);
        if (pause == false)
        {
            pauseSettingsUI.SetActive(false);
            HelpWindow.SetActive(false);
        }
        Player.playerObj.FPSEnable(!pause);
    }*/
	///runs pause function in pause script.
    public static void Pause() {
        pauseScript.pause();
    }
	///un pauses the game.
    public void unPause() {
        Player.closeWindow();
    }
	
	///Pauses the game and all states of the ship.
    public void pause() {
        Player.playerObj.FPSEnable(false);
        Debug.LogWarning("pausing");
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
        Player.openWindow(this);
    }
    #region Legacy pause
    /*unpauses game, can be called by button in pause menu itself
    public void Unpause()
    {
        //Debug.Log("Unpause");
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        pauseUI.gameObject.SetActive(false);
        pauseSettingsUI.SetActive(false);
        Player.playerObj.FPSEnable(true);
    }
    private void pause(){
        //Debug.Log("Pause");
        Player.playerObj.FPSEnable(false);
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
    }*/
    #endregion
    /// Use this for initialization
    void Awake() {
        Debug.Log(name);
        if (pauseScript == null) {
            pauseScript = this;
        } else {
            Destroy(transform.gameObject);
        }
    }
    void Start() {
        settings = new PauseSub(pauseSettingsUI);
        help = new PauseSub(pauseHelpUI);
        //showHelp();
    }
    // Update is called once per frame
    /*void Update()
    {
        if(count == 1 && !HelpWindow.activeInHierarchy)
        {
            count = 2;
        }
         if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (Player.pausable) setPause(!paused);
            }
    }*/
	
	///Shows the settings UI.
    public void showSettingsUI() {
        Debug.LogWarning("Show Settings");
        if (help.isVis()) {
            hideHelp();
        }
        if (!settings.isVis()) {
            settings.show();
            Player.openWindow(settings);
        }
    }
	
	///shows the help screen.
    public void showHelp() {
        Debug.LogWarning("Show Help");
        if (settings.isVis()) {
            hideSettingsUI();
        }
        if (!help.isVis()) {
            help.show();
            Player.openWindow(help);
        }
    }
	
	///hides the settings menu.
    public void hideSettingsUI()
    {
        Player.closeWindow();
    }
	///hides the help menu.
    public void hideHelp()
    {
        Player.closeWindow();
    }
	///closes the pause screen.
    public void close() {
        Player.playerObj.FPSEnable(true);
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}

public class PauseSub : Window {
	///gameobject to store UI
    GameObject ui;
	///bool to store if something is visible.
    bool Vis;
	///returns the vis variable, checking if something is visible.
    public bool isVis() {
        return Vis;
    }
	/// pauses UI and sets its visibility to false.
    public PauseSub(GameObject ui) {
        this.ui = ui;
        Vis = false;
    }
	///shows UI by setting its values to true.
    public void show() {
        Debug.LogWarning(ui.name);
        Vis = true;
        ui.SetActive(true);
    }
	///closes every by setting its visibility to false and sets active to false.
    public void close() {
        Vis = false;
        ui.SetActive(false);
    }
}
