using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {

    public Text t;
    string line = "";
    public Boolean service = true;
    public Boolean navDir = true;
    public Boolean isBroken = true;
    BreakEvent broken;

    List<string> console = new List<string>();
    public List<string> commands = new List<string>();
    int LINES = 6;


    public override void interact() {
        showUI(true);
        navDir = false;
    }

    public void onBreak() {
        throw new NotImplementedException();
    }

    protected override void initialise() {


        broken = new BreakEvent(this, 10);


        t = ui.GetComponentInChildren<Text>();
        t.text = "";

        //used to define a set of empty lines;
        

        commands.Add("cd /navigation");
        commands.Add("service stop navigation");
        commands.Add("mv navigation.back navigation");
        commands.Add("service start navigation");
        commands.Add("help");

        addline("Available Commands");
        foreach (string command in commands)
        {
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

        switch (entry)
        {

            case "cd /navigation":
                addline("cd /navigation");
                addline("moving to navigation directory");
                navDir = true;

                break;
            case "service stop navigation":
                addline("service stop navigation");
                if(service)
                {
                    addline("service stopping");
                    service = false;
                }
                else if(!service)
                {
                    addline("service currently inactive");
                }
                else
                {
                    addline("An error has occured please try again");
                }
                break;
            case "mv navigation.back navigation":
                addline("mv navigation.back navigation");
                if(navDir && !service)
                {
                    addline("Restoring back up files, service will not resume until it is started again");
                    isBroken = false;
                }
                else if(!navDir)
                {
                    addline("Have to be in navigation directory to use this command");
                }
                else
                {
                    addline("An error has occured please try again");
                }
                break;
            case "service start navigation":
                addline("service start navigation");
                if(service)
                {
                    addline("service is already started");
                }
                else if(!service)
                {
                    addline("starting service");
                    service = true;
                }
                break;
            case "help":
                addline("Available Commands");
                foreach (string command in commands)
                {
                    addline(command);
                }
                break;
            default:
                addline("Invalid Command, Please try again.");
                break;
        }

        



       }

    protected override void doUpdate() {
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
            t.text += ">" + line;
        }
    }

    public void onFix() {
        throw new NotImplementedException();
    }
}
