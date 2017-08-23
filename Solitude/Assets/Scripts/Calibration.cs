using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : Terminal, Breakable{

    Renderer rend;
    public int accuracy;
    public GameObject reticle;
    public GameObject square;
    public Text accuracyText;
    Material[] mat;
    bool isBroken;
    BreakEvent broken;

    public int ScreenElement;
    public Material matRedReff;
    Material matBlackReff;


    public override void interact()
    {
        if(rend != null) {
            mat[ScreenElement] = matRedReff;
            rend.materials = mat;
        }
        showUI(true);
    }

    public void onBreak()
    {
        if (rend != null)
        {
            mat[ScreenElement] = matRedReff;
            rend.materials = mat;
        }
    }

    protected override void initialise() {
        rend = GetComponent<Renderer>();
        mat = rend.materials;
        matBlackReff = mat[ScreenElement];
        broken = new BreakEvent(this, 100 - accuracy);
    }

    protected override void doUpdate () {
        accuracyText.text = "Accuracy: " + accuracy;
        reticle.transform.position = Input.mousePosition;

        if (reticle.transform.position == square.transform.position) //this is too tight a compare, won't actually work
        {
            //shrink
            square.transform.localScale = square.transform.localScale - (square.transform.localScale / 1000);

            //move
            Vector3 randDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0.0f); //set a random direction to move in
           // square.transform.localPosition move in that random direction

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                //This comparison also doesn't work
                if (reticle.transform.localScale.magnitude < square.transform.localScale.magnitude) {
                    accuracy = accuracy + (int)(reticle.transform.localScale.magnitude - square.transform.localScale.magnitude);
                }
            }
        }

        
	}

    public void onFix()
    {
        if (rend != null)
        {
            mat[ScreenElement] = matBlackReff;
            rend.materials = mat;
        }
        this.setActive(false);
        isBroken = false;
        showUI(false);
    }

}
