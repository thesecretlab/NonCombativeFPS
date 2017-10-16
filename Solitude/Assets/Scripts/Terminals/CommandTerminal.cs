using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {
    
	
	///Variable Declration
	public bool doBreak;
    Text t;
    string line = "";
    Boolean service = true;
    Boolean navDir = true;
    Boolean isBroken = true;
    BreakEvent broken;

    int dashCount = 0;
   
   ///The number of frames the dash is shown and not shown on
    int dashFrames = 20;


    ///List declarations to store all strings to be display on the screen.
	List<string> console = new List<string>();
	///List of commands the user can execute.
    List<string> commands = new List<string>();

	///Amount of lines to be printed to the screen.
    int LINES = 19;

	///This function will execute when the terminal is interacted with.
    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
            navDir = false;
        } else {
            Ship.ship.showAccess(true);
        }
    }
	///This function is called when the command terminal breaks.
    public void onBreak() {
        Toast.addToast("Navigation Corrupted. Please reset", 3);
        isBroken = false; //idk it was like this when I found it
        this.setActive(true);
        GameConditions.setTraveling(false);
    }
	///Initializes all variables.
    protected override void initialise() {
        broken = new BreakEvent(this, 20);
        t = ui.GetComponentInChildren<Text>();
        t.text = "";
		///Adds the following strings as commands to the command list.
        commands.Add("Go to Navigation");
        commands.Add("Stop Navigation");
        commands.Add("Load Navigation Backups");
        commands.Add("Start Navigation");
        commands.Add("Help");
        addline("Available Commands");
        
		///Adds enough lines to display all commands
        for (int i = commands.Count; i < LINES; i++) {
            addline("");
        }
		///Adds each command to the screen for the user to view.
        foreach (string command in commands) {
            addline(command);
        }
        onBreak();
    }

    ///used to add a line to the console;
    private void addline(string line) {
        console.Add(line);
        while (console.Count > LINES) {
            console.RemoveAt(0);
        }
    }

    ///called when the Return key is pressed
    private void onSubmit(string line) {
        
		///Adds the line that the user typed to the screen
		addline(line);
		///Makes the string the user entered all lower case for comparison.
        String entry = line.ToLower();
		///Trims whitespace of the string to get more accurate comparison results.
        entry = entry.Trim();
		
		///executes the following switch statement with the entry as the case and updates the command terminal if valid command is entered.
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
                if (!isBroken) {
                    service = true;
                    onFix();
                } else {
                    addline("Navigation Data Corrupted");
                    addline("Service Stopping");
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
        ///executes if the terminal is broken
		if (doBreak) {
            doBreak = false;
            onBreak();
        }
		
		///allows the entry of dyanamic characters onto the command screen.
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
    
	///This function executes when the command terminal is fixed.
    public void onFix() {
        GameConditions.setTraveling(true);
        this.setActive(false);
        isBroken = false;
        hide();
    }
    protected override void onClose() {

    }
}