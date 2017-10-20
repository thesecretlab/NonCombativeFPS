using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Link : MonoBehaviour {
    
	///image variable to store link image.
	private Image image;
	/// Use this for initialization
	void Awake () {
        image = GetComponent<Image>();
	}
	/// sets the color of the image.
    public void setColor(Color color) {
        image.color = color;
    }
}
