using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPanel : Terminal, Breakable {
    public int screws = 4;
    public int breakPercent = 100;
    public override void interact() {
        this.showUi();
    }

    public void onBreak() {
        this.setActive(true);
    }

    protected override void initialise() {
        Debug.Log("TT");
        new BreakEvent(this,breakPercent);
        for (int i = 0; i < screws; i++) {
            Debug.Log("Screwing");
            GameObject newscrew = Instantiate(this.ui.GetComponent<ScrewPanelControler>().screw, ui.transform);
            newscrew.transform.position = new Vector3(256, 128, 0);
        }
        ui.SetActive(true);
    }   

    public static void onScrew() {

    }
}
