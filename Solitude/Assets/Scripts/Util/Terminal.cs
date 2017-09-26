using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public abstract class Terminal : Interactable, Window {
    public GameObject uiPrefab;
    protected GameObject ui;
    protected abstract void initialise();
    protected abstract void doUpdate();
    public override abstract void interact();
    protected bool isVis;
    protected abstract void onClose();

    protected override void setup() {
        ui = Instantiate(uiPrefab);
        //Debug.LogWarning(name + ":" + uiPrefab.name);
        ui.transform.SetParent(UICanvas.Canvas.transform, false);
        this.initialise();
        ui.SetActive(false);
        isVis = false;
    }
    void Update() {
        doUpdate();
    }
    protected void show() {
        Debug.Log("Open " + name);
        ui.SetActive(true);
        Player.openWindow(this);
        Player.playerObj.FPSEnable(false);
        isVis = true;
    }
    public void hide() {
        Player.closeWindow();
    }

    public void close() {
        onClose();
        Player.playerObj.FPSEnable(true);
        ui.SetActive(false);
        Player.playerObj.FPSEnable(true);
        isVis = false;
    }
}
