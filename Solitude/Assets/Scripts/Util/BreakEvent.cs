using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Breakable {
    void onBreak();
    void onFix();
}

public class BreakEvent {
    Breakable parent;
    
    public int breakPercent;

    public BreakEvent(Breakable parent,int breakPercent, bool startBroke = false) {
        this.parent = parent;
        this.breakPercent = breakPercent;
        try {
            Ship.ship.addBreakEvent(this,startBroke);
        } catch (System.NullReferenceException e) {
            Debug.Log("Ship not found");
            Debug.Log(e.ToString());
        }
    }

    public void dobreak() {
        parent.onBreak();
    }
}
