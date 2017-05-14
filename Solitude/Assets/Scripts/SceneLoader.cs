using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("HighScoreScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadSettingsPage()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
