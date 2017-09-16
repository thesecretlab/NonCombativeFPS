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
}
