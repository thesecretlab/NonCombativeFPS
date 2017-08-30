using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : Terminal, Breakable{

    Renderer rend;
    public int accuracy = 100;
    public GameObject reticle;
    public GameObject square;
    public Text accuracyText;
    Material[] mat;
    bool isBroken;
    BreakEvent broken;

    public int ScreenElement;
    public Material matRedReff;
    Material matBlackReff;
    Vector3 randDir;

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
        randDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0.0f);
    }

    protected override void doUpdate () {
        accuracyText.text = "Accuracy: " + accuracy; //accuracy text updating
        reticle.transform.position = Input.mousePosition; //move circle with mouse

        //move square in random direction
        square.transform.localPosition = (randDir - square.transform.localPosition).normalized * 10 * Time.deltaTime;

        if (Vector3.Distance(reticle.transform.position, square.transform.position) <= 4) //TODO:square and circle not overlapping
        {
            if (1!=1) //TODO:square touches the edge of the canvas
            {
                randDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0.0f);
            }

        }
        else if (1==1)//TODO:square and circle are overlapping oh baby
        {
            //shrink
            square.transform.localScale = square.transform.localScale - (square.transform.localScale / 1000);


            if (Input.GetKeyDown(KeyCode.KeypadEnter)) //TODO: change to mouse down
            {
                //TODO: This comparison doesn't really work
                if (reticle.transform.localScale.magnitude < square.transform.localScale.magnitude)
                {
                    //TODO: need to make this way more accurate
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
