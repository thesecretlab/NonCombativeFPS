using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief The terminal class for power rerouteing
/// 
public class PowerTerminal : Terminal {
    /// 
    /// \brief Called when the player interacts with the terminal
    /// 
    /// \return No return value
    /// 
    /// \details Shows the power UI or access revoked panel.
    /// 
    public override void interact() {
        if (Ship.ship.getAccess()) {
            show();
        } else {
            Ship.ship.showAccess(true);
        }
    }
    /// 
    /// \brief Called once per frame
    /// 
    /// \return No return value
    /// 
    /// \details Needed by Terminal base class. No actual functionality
    /// 
    protected override void doUpdate() {
        
    }
    /// 
    /// \brief Used for initialisation
    /// 
    /// \return No return value
    /// 
    /// \details Sets the UI
    /// 
    protected override void initialise() {
        PowerSystem.setUI(ui);
    }
    /// 
    /// \brief Called when the UI is closed
    /// 
    /// \return No return value
    /// 
    /// \details Required by Terminal base class. No actual functionality
    /// 
    protected override void onClose() {

    }
}
