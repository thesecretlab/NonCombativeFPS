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

    //unpauses game, can be called by button in pause menu itself
    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        pauseUI.gameObject.SetActive(false);
        pauseSettingsUI.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        Unpause();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Escape))
        {
            paused = true;
        }

        if (paused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            pauseUI.gameObject.SetActive(true);
        }
         else //I have no idea why this needs to be here but it makes it work the way it should
        {
            Unpause();
        }
    }

    public void showSettingsUI()
    {
        pauseSettingsUI.SetActive(true);
    }

    public void hideSettingsUI()
    {
        pauseSettingsUI.SetActive(false);
    }

}
