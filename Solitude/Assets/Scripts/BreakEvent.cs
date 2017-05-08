using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Breakable {
    void onBreak();
}

public class BreakEvent{
    Breakable parent;
    public int breakPercent;
    public BreakEvent(Breakable parent,int breakPercent) {
        this.parent = parent;
        this.breakPercent = breakPercent;
        ((Ship)Object.FindObjectOfType<Ship>()).addBreakEvent(this);
    }

    public void dobreak() {
        parent.onBreak();
    }
}
