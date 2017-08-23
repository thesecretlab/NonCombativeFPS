using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathwayActivation : MonoBehaviour {
    
    public Button Button1;
    public Button Button2;
    public Image Pathway;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Button1 == true && Button2 == true)
        {
            Pathway.GetComponent<Image>().color = Color.cyan;
        }
	}
}
