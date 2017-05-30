using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitching : MonoBehaviour {

    //code adapted from https://www.youtube.com/watch?v=Dn_BUIVdAPg&ab_channel=Brackeys
    //items should be made a child of the itemholder object on the player for this to work

    public int selectedItem = 0;

    // Use this for initialization
    void Start () {
        SelectItem();
	}

    // Update is called once per frame
    void Update() {

        int prevSelectedItem = selectedItem;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedItem >= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedItem <= 0)
                selectedItem = transform.childCount - 1;
            else
                selectedItem--;
        }

        if (prevSelectedItem != selectedItem)
        {
            SelectItem();
        }

        //can be expanded up to 9 if needed by copy-pasting the second statement and incrementing values
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedItem = 1;
        }



    }

    void SelectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selectedItem)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            i++;
        }

    }
}
