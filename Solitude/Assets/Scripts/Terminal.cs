using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : Interactable {
    public GameObject uiPrefab;
    private GameObject ui;
    public override void interact() {
        Debug.Log("Terminal Incorectly setup: " + transform.gameObject.name);
    }

    protected override void setup() {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        ui = Instantiate(uiPrefab);
        ui.transform.SetParent(canvas.transform, false);
        ui.SetActive(false);
    }
}
