using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class SystemDefenseFirewall : MonoBehaviour {

    public GameObject Button;
 
    public Sprite Firewall;

     public void activate_defense_Fire()
    {

        Button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
        Button.GetComponent<Image>().sprite = Firewall;
        GlobalVars.GlobalVariables.INACCESSIBLE = 1;
    }
}
