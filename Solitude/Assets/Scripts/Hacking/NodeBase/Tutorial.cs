using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

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
