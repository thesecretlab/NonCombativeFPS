using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNodeColor : MonoBehaviour {

  
    public GameObject Button; 
    public void changeColor()
    {
        Button.GetComponent<Image>().color = Color.cyan;
 
    }
}
