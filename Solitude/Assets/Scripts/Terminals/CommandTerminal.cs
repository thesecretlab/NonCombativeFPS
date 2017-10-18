using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {
    
	
	///stores if the current terminal is broken
	public bool doBreak;
    ///Stores a text object.
	Text t; 
	///a blank line to add to the console
    string line = "";
	/// Tracks if the service is current active.
    Boolean service = true; 
	/// Tracks if the user is currently in the naigation directory.
    Boolean navDir = true; 
	/// stores if the navigation is broken
    Boolean isBroken = true; 
	///Variable to store the breakevent
    BreakEvent broken;
	///Stores the amount of dashes
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
        Toast.addToast("Navigation Corrupted. Please reset", 3); ///when the navigation is corrupted add it to the top of the screen
        isBroken = false; //idk it was like this when I found it
        this.setActive(true);
        GameConditions.setTraveling(false);///sets traveling to false in the game conditions script.
    }
	///Initializes all variables.
    protected override void initialise() {
        broken = new BreakEvent(this, 20); ///creates a new break event
        t = ui.GetComponentInChildren<Text>(); ///gets the text component and all children and stores it in t.
        t.text = ""; /// sets the text field t to be blank.
		
		///Adds the following strings as commands to the command list.
        commands.Add("Go to Navigation");
        commands.Add("Stop Navigation");
        commands.Add("Load Navigation Backups");
        commands.Add("Start Navigation");
        commands.Add("Help");
        addline("Available Commands");
        
		///Adds enough lines to terminal to display all commands
        for (int i = commands.Count; i < LINES; i++) {
            addline("");
        }
		///Adds each command to the terminal for the user to view.
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
				///Goes to the navigation directory by setting the value to true and telling the user what has happened.
                addline("moving to navigation directory");
                navDir = true;
                break;
            case "stop navigation":
				/// addlines the line to tell the user what is happening and setting the service to false, stopping it.
                addline("service stopping");
                service = false;
                break;
            case "load navigation backups":
				///Loads the navigation route from the current back ups, this can only be done if the user is in the nav directory and the service is stopped.
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
				///starts the service again, has to be done after back ups are loaded.
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
				///Displays all commands the user can use.		
                addline("Available Commands");
                foreach (string command in commands) {
                    addline(command);
                }
                break;
            default:
				///this will be shown when a invalid command is inputted.	
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