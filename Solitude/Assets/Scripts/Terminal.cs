using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public abstract class Terminal : Interactable {
    public GameObject uiPrefab;
    public GameObject ui;
    protected abstract void initialise();

    private bool isVis;

    public override abstract void interact();

    protected override void setup() {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        ui = Instantiate(uiPrefab);
        ui.transform.SetParent(canvas.transform, false);
        ui.SetActive(false);
        isVis = false;
        this.initialise();
    }

    void Update() {
        if (isVis & CrossPlatformInputManager.GetButtonDown("Esc")) {
            showUI(false);
        }
    }

    protected void showUI(bool show) {
        ui.SetActive(show);
        isVis = show;
        Player.playerObj.FPSEnable(!show);
    }
}
