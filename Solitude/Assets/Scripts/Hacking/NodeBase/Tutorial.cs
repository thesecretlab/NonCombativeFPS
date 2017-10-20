using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	///stores a gameobject to store the tutorial window;.
    public GameObject window;

	///shows the window.
    public void Show()
    {
        window.SetActive(true);
    }
	///hides the window.
    public void Hide()
    {
        window.SetActive(false);
    }
}
