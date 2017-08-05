﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {
    Animator anim;

    Reactor rec;
    public int num;

    bool blown;

    public string getName() {
        return gameObject.name;
    }

    public void blow() {
        if (!blown) {
            anim.SetTrigger("blow");
            blown = true;
            active = true;
        }
    }

    public bool isBlown() {
        return blown;
    }

    public override void interact() {
        if (blown) {
            active = false;
            anim.SetTrigger("pull");
            blown = false;
            rec.throwLever(num);
        }
    }

    public void setReactor(Reactor rec,int num) {
        this.rec = rec;
        this.num = num;
    }

    protected override void setup() {
        active = false;
        anim = GetComponent<Animator>();
        blown = false;
    }
}