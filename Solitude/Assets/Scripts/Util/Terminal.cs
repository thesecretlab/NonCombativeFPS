using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public abstract class Terminal : Interactable {
    public GameObject uiPrefab;
    protected GameObject ui;
    protected abstract void initialise();
    protected abstract void doUpdate();
    protected bool isVis;
    public override abstract void interact();

    protected override void setup() {
        ui = Instantiate(uiPrefab);
        ui.transform.SetParent(UICanvas.Canvas.transform, false);
        ui.SetActive(false);
        isVis = false;
        this.initialise();
    }
    void Update() {
        if (isVis & CrossPlatformInputManager.GetButtonDown("Esc")) {
            showUI(false);
        }
        doUpdate();
    }
    protected void showUI(bool show) {
        ui.SetActive(show);
        isVis = show;
        Player.playerObj.FPSEnable(!show);
    }
}
