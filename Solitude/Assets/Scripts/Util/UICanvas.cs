using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour {

    public static UICanvas Canvas;

	// Use this for initialization
	void Awake () {
        if (Canvas == null) {
            Canvas = this;
        } else {
            Destroy(transform.gameObject);
        }
	}

    public static float getWidth() {
        //Debug.Log(Canvas.GetComponent<RectTransform>().rect.width);
        return Canvas.GetComponent<RectTransform>().rect.width;
    }
    public static float getHeight() {
        //Debug.Log(Canvas.GetComponent<RectTransform>().rect.height);
        return Canvas.GetComponent<RectTransform>().rect.height;
    }
}
