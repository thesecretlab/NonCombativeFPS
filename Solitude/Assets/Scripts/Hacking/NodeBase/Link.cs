using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Link : MonoBehaviour {
    private Image image;
	// Use this for initialization
	void Awake () {
        image = GetComponent<Image>();
	}

    public void setColor(Color color) {
        image.color = color;
    }
}
