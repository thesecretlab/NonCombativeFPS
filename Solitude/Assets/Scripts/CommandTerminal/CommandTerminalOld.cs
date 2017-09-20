using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminalOld : Terminal, Breakable {

    Text t;
    string line = "";
    Boolean service = true;
    Boolean navDir = true;
    Boolean isBroken = true;
    BreakEvent broken;

    public int ScreenElement;
    public Material matRedReff;
    Material matBlackReff;
    Renderer rend;
    Material[] mat;
    int dashCount = 0;
    //the number of frames the dash is shown and not shown on
    int dashFrames = 20;

    public bool backspacepressed = false;

    List<string> console = new List<string>();

    public List<string> commands = new List<string>();

    int LINES = 6;

    public override void interact() {
        if (rend != null) {
            mat[ScreenElement] = matRedReff;
            rend.materials = mat;
        }
        showUI(true);
        navDir = false;
    }

    public void onBreak() {
        if (rend != null) {
            mat[ScreenElement] = matRedReff;
            rend.materials = mat;
        }
    }

    protected override void initialise() {
        rend = GetComponent<Renderer>();
        mat = rend.materials;
        matBlackReff = mat[ScreenElement];
        broken = new BreakEvent(this, 50);
        t = ui.GetComponentInChildren<Text>();
        t.text = "";
        commands.Add("Go to Navigation");
        commands.Add("Stop Navigation");
        commands.Add("Load Navigation Backups");
        commands.Add("Start Navigation");
        commands.Add("Help");
        addline("Available Commands");
        foreach (string command in commands) {
            addline(command);
        }
        for (int i = commands.Count; i < LINES; i++) {
            addline("");
        }
    }

    //used to add a line to the console;
    private void addline(string line) {
        console.Add(line);
        while (console.Count > LINES) {
            console.RemoveAt(0);
        }
    }

    //called when the Return key is pressed
    private void onSubmit(string line) {
        addline(line);
        String entry = line.ToLower();
        entry = entry.Trim();
        switch (entry) {
            case "Go to Navigation":
                addline("moving to navigation directory");
                navDir = true;
                break;
            case "Stop Navigation":
                addline("service stopping");
                service = false;
                break;
            case "Load Navigation Backups":
                if (!navDir) {
                    addline("Unable to locate Navigation back-ups");
                    break;
                }
                if (service) {
                    addline("File Navigation is in use");
                    break;
                }
                addline("Restoring back up files");
                isBroken = false;
                break;
            case "Start Navigation":
                addline("starting service");
                service = true;
                if (!isBroken) {
                    onFix();
                }
                break;
            case "Help":
                addline("Available Commands");
                foreach (string command in commands) {
                    addline(command);
                }
                break;
            default:
                addline("Invalid Command, Please try again.");
                break;
        }
    }
    protected override void doUpdate() {
        dashCount++;
        if (dashCount > 2 * dashFrames) {
            dashCount -= 2 * dashFrames;
        }
        if (isVis) {
            
            foreach(char c in Input.inputString) {
                if(c == "\b"[0]) {
                    if (line.Length > 0) {
                        line = line.Substring(0, line.Length - 1);
                    }
                } else if (c == "\n"[0] || c == "\r"[0]) {
                    onSubmit(line);
                    line = "";
                } else {
                    line += c;
                }
            }
            t.text = "";
            foreach (string l in console) {
                t.text += "   " + l + "\n";
            }
            t.text += ">";
            t.text += line;
            
            if (dashCount < dashFrames) { t.text += "_"; };
        }
    }
    
    public void onFix() {
        if (rend != null) {
            mat[ScreenElement] = matBlackReff;
            rend.materials = mat;
        }
        this.setActive(false);
        isBroken = false;
        showUI(false);
    }

    protected override void onClose() {
        showUI(false);
    }
}