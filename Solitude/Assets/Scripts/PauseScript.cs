using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public bool paused = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
