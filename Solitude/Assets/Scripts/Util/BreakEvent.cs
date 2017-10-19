using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// \brief An interface for breakable objects
public interface Breakable {
    void onBreak();
    void onFix();
}

/// 
/// \brief A Class sitting in the middle of a breakable object and the auto break system
/// 
public class BreakEvent {
	/// The breakable object
    Breakable parent;
    /// The percentage that the object will break every 10 seconds
    public int breakPercent;
    /// 
    /// \brief The constructor of the break event
    /// 
    /// \param [in] parent The breakable object
    /// \param [in] breakPercent The percentage chance of the object breaking
    /// \param [in] startBroke If the object should start broken. Default false
    /// \return Returns the new Breakevent
    /// 
    /// \details Constructing a break event will automatically add that event to the auto-break system
    /// 
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
    /// 
    /// \brief Called to break the breakable object
    /// 
    /// \return No return value
    /// 
    /// \details 
    /// 
    public void dobreak() {
        parent.onBreak();
    }
}
