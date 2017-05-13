using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Terminal : Interactable {
    public GameObject uiPrefab;
    protected GameObject ui;
    protected abstract void initialise();

    public override abstract void interact();

    protected override void setup() {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        ui = Instantiate(uiPrefab);
        ui.transform.SetParent(canvas.transform, false);
        ui.SetActive(false);
        this.initialise();
    }

    protected void showUi() {
        ui.SetActive(true);
    }


}