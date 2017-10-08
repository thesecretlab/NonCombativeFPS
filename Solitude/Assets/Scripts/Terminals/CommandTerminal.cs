using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {
    public bool doBreak;
    Text t;
    string line = "";
    Boolean service = true;
    Boolean navDir = true;
    Boolean isBroken = true;
    BreakEvent broken;

    int dashCount = 0;
    //the number of frames the dash is shown and not shown on
    int dashFrames = 20;

    //bool backspacepressed = false;

    List<string> console = new List<string>();

    List<string> commands = new List<string>();

    int LINES = 19;

    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
            navDir = false;
        } else {
            Ship.ship.showAccess(true);
        }
    }

    public void onBreak() {
        Toast.addToast("Navigation Corrupted. Please reset", 3);
        isBroken = false;
        GameConditions.setTraveling(false);
    }

    protected override void initialise() {
        broken = new BreakEvent(this, 20);
        t = ui.GetComponentInChildren<Text>();
        t.text = "";
        commands.Add("Go to Navigation");
        commands.Add("Stop Navigation");
        commands.Add("Load Navigation Backups");
        commands.Add("Start Navigation");
        commands.Add("Help");
        addline("Available Commands");
        
        for (int i = commands.Count; i < LINES; i++) {
            addline("");
        }
        foreach (string command in commands) {
            addline(command);
        }
        onBreak();
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
            case "go to navigation":
                addline("moving to navigation directory");
                navDir = true;
                break;
            case "stop navigation":
                addline("service stopping");
                service = false;
                break;
            case "load navigation backups":
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
            case "start navigation":
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
        if (doBreak) {
            doBreak = false;
            onBreak();
        }
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
        GameConditions.setTraveling(true);
       // this.setActive(false);
        isBroken = false;
        hide();
    }
    protected override void onClose() {

    }
}