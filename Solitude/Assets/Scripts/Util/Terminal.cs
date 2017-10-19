using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// 
/// \brief An inheritable class for all terminal objects
/// 
public abstract class Terminal : Interactable, Window {
	/// The prefab for the UI
    public GameObject uiPrefab;
	/// The UI created from the prefab
    protected GameObject ui;
    /// 
    /// \brief Used by inheritors for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    protected abstract void initialise();
    /// 
    /// \brief Used by inheritors for Update
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    protected abstract void doUpdate();
    /// 
    /// \brief Used buy inheritors for interaction
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    public override abstract void interact();
	/// If the attached UI is visible
    protected bool isVis;
    /// 
    /// \brief Used by inheritors for when the ui iis closing
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    protected abstract void onClose();
    /// 
    /// \brief Used for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Creates the UI form prefab
    /// 
    protected override void setup() {
        ui = Instantiate(uiPrefab);
        //Debug.LogWarning(name + ":" + uiPrefab.name);
        ui.transform.SetParent(UICanvas.Canvas.transform, false);
        this.initialise();
        ui.SetActive(false);
        isVis = false;
    }
    /// 
    /// \brief Called once per frame
    /// 
    /// \return No return value
    /// 
    /// \details Calls inheritors update
    /// 
    void Update() {
        doUpdate();
    }
    /// 
    /// \brief Shows the UI
    /// 
    /// \return No return value
    /// 
    /// \details Disables the FPSControler and enables the mouse
    /// 
    protected void show() {
        Debug.Log("Open " + name);
        ui.SetActive(true);
        Player.openWindow(this);
        Player.playerObj.FPSEnable(false);
        isVis = true;
    }
    /// 
    /// \brief Hides the UI
    /// 
    /// \return No return value
    /// 
    /// \details Gets the player object to close the window
    /// 
    public void hide() {
        Player.closeWindow();
    }
    /// 
    /// \brief Called when the window is closed
    /// 
    /// \return No return value
    /// 
    /// \details Enables theFPSControler and disables the mouse
    /// 
    public void close() {
        onClose();
        Player.playerObj.FPSEnable(true);
        ui.SetActive(false);
        isVis = false;
    }
}
