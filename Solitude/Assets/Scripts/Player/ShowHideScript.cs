using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enables and disables a gameobject. Primarily used in the pause screen.
/// </summary>

public class ShowHideScript : MonoBehaviour {

    public GameObject window;


    public void Show()
    {
        window.SetActive(true);
    }

    public void Hide()
    {
        window.SetActive(false);
    }

}
