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


    public override void interact() {
        throw new NotImplementedException();
    }

    public void onBreak() {
        throw new NotImplementedException();
    }

    protected override void initialise() {
        t = ui.GetComponentInChildren<Text>();
        t.text = "";
        //used to define a set of empty lines;
        console = new List<string>(new string[20]);
    }

    //used to add a line to the console;
    private void addline(string line) {
        console.Add(line);
        if (console.Count > 20) {
            console.RemoveAt(0);
        }
    }

    //called when the Return key is pressed
    private void onSubmit(string line) {
        addline(line);

    }

    void doUpdate() {
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
