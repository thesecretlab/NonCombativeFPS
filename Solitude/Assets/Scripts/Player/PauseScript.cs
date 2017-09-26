using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseScript :MonoBehaviour, Window {
    //track if game is paused
    //public bool paused;
    //used to point to the pause ui
    public GameObject pauseUI;
    static PauseScript pauseScript;
    //used to point to the settings ui
    public GameObject pauseSettingsUI;
    public GameObject pauseHelpUI;
    PauseSub settings, help;
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

    public static void Pause() {
        pauseScript.pause();
    }

    public void unPause() {
        Player.closeWindow();
    }
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
    // Use this for initialization
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
    public void hideSettingsUI()
    {
        Player.closeWindow();
    }

    public void hideHelp()
    {
        Player.closeWindow();
    }

    public void close() {
        Player.playerObj.FPSEnable(true);
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}
public class PauseSub : Window {
    GameObject ui;
    bool Vis;
    public bool isVis() {
        return Vis;
    }
    public PauseSub(GameObject ui) {
        this.ui = ui;
        Vis = false;
    }
    public void show() {
        Debug.LogWarning(ui.name);
        Vis = true;
        ui.SetActive(true);
    }
    public void close() {
        Vis = false;
        ui.SetActive(false);
    }
}
