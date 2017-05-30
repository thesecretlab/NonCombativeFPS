using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {

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
        broken = new BreakEvent(this, 10);
        t = ui.GetComponentInChildren<Text>();
        t.text = "";
        commands.Add("cd /navigation");
        commands.Add("service stop navigation");
        commands.Add("mv navigation.back navigation");
        commands.Add("service start navigation");
        commands.Add("help");
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
        switch (entry.ToLower()) {
            case "cd /navigation":
                addline("moving to navigation directory");
                navDir = true;
                break;
            case "service stop navigation":
                addline("service stopping");
                service = false;
                break;
            case "mv navigation.back navigation":
                if (!navDir) {
                    addline("Unable to locate Navigation.back");
                    break;
                }
                if (service) {
                    addline("File Navigation is in use");
                    break;
                }
                addline("Restoring back up files");
                isBroken = false;
                break;
            case "service start navigation":
                addline("starting service");
                service = true;
                if (!isBroken) {
                    onFix();
                }
                break;
            case "help":
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
            if (Input.GetKeyDown(KeyCode.Return)) {
                onSubmit(line.Trim());
                line = "";
            }
            if (Input.GetKey(KeyCode.Backspace)) {
                if (line.Length > 0) {
                    line = line.Substring(0, line.Length - 1);
                }
            }
            line += Input.inputString;
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
        isBroken = false;
        showUI(false);
    }
}