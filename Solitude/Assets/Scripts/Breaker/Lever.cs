using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {
    Animator anim;

    Reactor rec;
    public int num;

    public override void interact() {
        anim.SetTrigger("pull");
        rec.throwLever(num);
    }

    public void setReactor(Reactor rec,int num) {
        this.rec = rec;
        this.num = num;
    }

    protected override void setup() {
        anim = GetComponent<Animator>();
    }
}
