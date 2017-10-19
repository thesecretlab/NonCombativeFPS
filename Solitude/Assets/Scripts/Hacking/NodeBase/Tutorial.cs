using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///Controls the hiding and showing of the hackinggame tutorial window
///<summary>



public class Tutorial : MonoBehaviour {
	
	///Variable holding game object window 
    public GameObject window;

	///function shows the window when called
    public void Show()
    {
		///window is set active in heirachy
        window.SetActive(true);
    }

	///function hides the window when called
    public void Hide()
    {
		///window is deactivated in heirarchy
        window.SetActive(false);
    }
}
