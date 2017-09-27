using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
