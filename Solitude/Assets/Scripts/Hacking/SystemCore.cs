using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class SystemCore : MonoBehaviour {

    public GameObject Button;
 

    public Sprite systemcore;

    public void activate_system_core()
    {
        Button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
        Button.GetComponent<Image>().sprite = systemcore;
        GlobalVars.GlobalVariables.SYSCORE_FOUND = 1;    
    }
}
