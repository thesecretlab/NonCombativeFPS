using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if an item can be given from a crate, and if so, gives a random to the player
/// </summary>

public class CrateGiveItem : Interactable {

    public Inventory inv;
    public bool canGiveItem = true;

    protected override void setup()
    {
    }

    public override void interact()
    {
        if (canGiveItem)
        {
            inv.giveRandomItem(1, 2);
            canGiveItem = false;
        }
    }
}
