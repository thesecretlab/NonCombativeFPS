using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class SystemDefenseFire : MonoBehaviour {

    public GameObject Button;
 
    public Sprite Firewall;

    public void activate_defense_Fire()
    {

        Button.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
        Button.GetComponent<Image>().sprite = Firewall;
        GlobalVars.GlobalVariables.INACCESSIBLE = 1;
    }

   /* public Button[] buttons;
    int numDefenses = 0;
    public int maxDefenses;

    buttons = GetComponentsInChildren<Button>();
    foreach (Button butt in buttons){
        int isDefense = rand(0, Arraylen);
    if (isDefense > (Arraylen / 2) && numDefenses < maxDefenses){
            script DefenceFire = gameobject.addcomponent<Scriptname>;
            numDefenses++;
        }
        */


}
