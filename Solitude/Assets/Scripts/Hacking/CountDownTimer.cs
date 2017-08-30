using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CountDownTimer : Terminal
{
    float timeLeft = 50.0f;
    float closetime = 3.0f;
    bool Gameover = false;

    public Text text;

    protected override void doUpdate()
    {
        if (!Gameover)
        {
            timeLeft -= Time.deltaTime;
            text.text = "Time Remaining:" + Mathf.Round(timeLeft);
        }

        if (timeLeft < 0)
        {
            Gameover = true;
            text.text = "#!HACK FAILED!#";
            closetime -= Time.deltaTime;
            SceneManager.LoadScene("MainGame");

        }

        if(GlobalVars.GlobalVariables.SYSCORE_FOUND == 1)
        {
            Gameover = true;
            text.text = "HACK SUCCESSFUL";
            closetime -= Time.deltaTime;
            SceneManager.LoadScene("MainGame");
        }

    }

    protected override void initialise()
    {
        throw new NotImplementedException();
    }

    public override void interact()
    {
        throw new NotImplementedException();
    }
}