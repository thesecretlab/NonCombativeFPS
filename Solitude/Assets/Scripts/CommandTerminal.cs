using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandTerminal : Terminal, Breakable {

    public Text t;
    string line = "";

    List<string> console = new List<string>();
    public List<string> commands = new List<string>();
    int LINES = 6;


    public override void interact() {
        showUI(true);
    }

    public void onBreak() {
        throw new NotImplementedException();
    }

    protected override void initialise() {

        t = ui.GetComponentInChildren<Text>();
        t.text = "";

        //used to define a set of empty lines;
        

        commands.Add("cd /navigation");
        commands.Add("service stop navigation");
        commands.Add("mv navigation.back navigation");
        commands.Add("service start navigation");

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

        switch (line.ToLower())
        {

            case "cd /navigation":
                addline("cd /navigation");
                break;
            case "service stop navigation":
                addline("service stop navigation");
                break;
            case "mv navigation.back navigation":
                addline("mv navigation.back navigation");
                break;
            case "service start navigation":
                addline("service start navigation");
                break;
            default:
                addline("Invalid Command, Please try again.");
                break;
        }

        



       }

    protected override void doUpdate() {
        if (isVis) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                onSubmit(line);
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
            t.text += "> " + line;
        }
    }

    public void onFix() {
        throw new NotImplementedException();
    }
}
