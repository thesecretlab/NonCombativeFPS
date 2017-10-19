using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// \brief Test interactable class
/// 
public class Interface : Interactable {

    public override void interact() {
        Debug.Log("YAY");
    }

    protected override void setup() {

    }
}
