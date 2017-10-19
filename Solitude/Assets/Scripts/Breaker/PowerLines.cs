using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief The core of the Breakers mini-game
/// 
public class PowerLines : MonoBehaviour, Breakable {
	/// An array of the levers
    private Lever[] levers;
	/// If the breakers are fixed
    private bool @fixed = true;

    /// 
    /// \brief Gets if the levers are fixed
    /// 
    /// \return Returns if the levers are fixed
    /// 
    /// \details 
    /// 
    public bool getFixed() {
        return @fixed;
    }
    /// 
    /// \brief Called when the breakers are broken
    /// 
    /// \return No return value
    /// 
    /// \details Inherited from the Breakable interface
    /// 
    public void onBreak() {
        @fixed = false;
        foreach(Lever lev in levers) {
            //Debug.Log(lev.getName());
            lev.blow();
        }
    }
    /// 
    /// \brief Called when the breakers are fixed
    /// 
    /// \return No return value
    /// 
    /// \details Inherited from the Breakable interface
    /// 
    public void onFix() {
        Debug.Log("Breakers Fixed");
        @fixed = true;
    }
    /// 
    /// \brief Called when a lever is thrown
    /// 
    /// \param [in] lever The ID of the lever that was thrown
    /// \return No return value
    /// 
    /// \details Calles onFix if all levers have been thrown
    /// 
    public void throwLever(int lever) {
        bool isblown = false;
        foreach (Lever lev in levers) {
            if (!isblown) {
                if (lev.isBlown()) {
                    isblown = true;
                }
            }
        }
        if (!isblown) {
            onFix();
        }
    }
    /// 
    /// \brief Used for initialization
    /// 
    /// \return No return value
    /// 
    /// \details Gets all lever children and sets itself as the powerLines
    /// 
    void Start () {
        //new BreakEvent(this, 10);
		levers = GetComponentsInChildren<Lever>();
        int i = 0;
        foreach (Lever lev in levers) {
            lev.setLines(this,i);
            i++;
        }
	}
}
