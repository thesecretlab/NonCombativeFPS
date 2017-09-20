using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameConditions : MonoBehaviour {

    public float Time = 600.0f;
    public Text text;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        Time -= UnityEngine.Time.deltaTime;
        text.text = "Time \nRemaining:  " + Mathf.Round(Time);
    }
}
