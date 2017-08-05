using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    //track if game is paused
    public bool paused;

    //used to point to the pause ui
    public GameObject pauseUI;
    //used to point to the settings ui
    public GameObject pauseSettingsUI;

    public void setPause(bool pause) {
        Time.timeScale = System.Convert.ToInt32(!pause);
        paused = pause;
        pauseUI.gameObject.SetActive(pause);
        if (pause == false) { pauseSettingsUI.SetActive(false); }
        Player.playerObj.FPSEnable(!pause);
    }

    #region Legacy pause
    //unpauses game, can be called by button in pause menu itself
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
    }
    #endregion

    // Use this for initialization
    void Start() {
        Unpause();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setPause(!paused);
        }
    }

    public void showSettingsUI()
    {
        pauseSettingsUI.SetActive(true);
    }

    public void hideSettingsUI()
    {
        Debug.Log("hide settings");
        pauseSettingsUI.SetActive(false);
    }

}
