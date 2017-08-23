using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    float timeLeft = 10.0f;
    float closetime = 3.0f;

    public Text text;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "Time Remaining:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            text.text = "#!HACK FAILED!#";
            closetime -= Time.deltaTime;
            SceneManager.LoadScene("MainGame");

        }
    }
}