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
        show();
    }

    public void onBreak()
    {
        if (rend != null)
        {
            if (accuracy < 20)
            {
                accuracy =  (accuracy/2);
            }
            mat[ScreenElement] = matRedReff;
            rend.materials = mat;
        }
    }

    protected override void initialise() {
        rend = GetComponent<Renderer>();
        mat = rend.materials;
        matBlackReff = mat[ScreenElement];
        broken = new BreakEvent(this, 90 - accuracy); //should be fine, but this ~might~ cause a crash somewhere
        randDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0.0f);
    }

    protected override void doUpdate () {
        accuracyText.text = "Accuracy: " + accuracy; //accuracy text updating
        reticle.transform.position = Input.mousePosition; //move circle with mouse

        //move square in random direction
        square.transform.localPosition = (randDir - square.transform.localPosition).normalized * 10 * Time.deltaTime;

        //square and circle not overlapping
        if (Vector3.Distance(reticle.transform.position, square.transform.position) > square.transform.localScale.magnitude) 
        {
            Debug.Log("Not touching");
            //It does get to here
            if (1!=1) //TODO:square touches the edge of the canvas
            {
                randDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0.0f);
            }

        }
        //square and circle are overlapping oh baby
        else if (Vector3.Distance(reticle.transform.position, square.transform.position) <= square.transform.localScale.magnitude)
        {
            Debug.Log("touching");
            //shrink
            square.transform.localScale = square.transform.localScale - (square.transform.localScale / 500);

            if (Input.GetMouseButtonDown(0)) //player is updating the accuracy value
            {
                //TODO: This comparison doesn't really work
                if (reticle.transform.localScale.magnitude < square.transform.localScale.magnitude)
                {
                    //TODO: need to make this way more accurate
                    accuracy = (int)(100 - Mathf.Abs(reticle.transform.localScale.magnitude - square.transform.localScale.magnitude));
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
        show();
    }

    protected override void onClose() {
    }
}
