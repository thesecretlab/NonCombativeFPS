using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretUI : MonoBehaviour {
    public Target target;
    public Text accuracyNumber;

    TurretTerminal TT;

    float width;
    float height;

    float cWidth;
    float cHeight;
    Vector3 scale;
    float randomX;
    float randomY;
    float change;
    float moveSpeed;
    bool overTarget;

    private Rigidbody2D rb2d;
    
    // Use this for initialization
    void Awake()
    {
        rb2d = target.GetComponent<Rigidbody2D>();
        width = GetComponentInParent<RectTransform>().rect.width;
        height = GetComponentInParent<RectTransform>().rect.height;
        target.setUI(this);
    }
    public void setTerminal(TurretTerminal tt) {
        TT = tt;
    }

    public void setOverTarget()
    {
        overTarget = true;
    }

    public void setLostTarget()
    {
        overTarget = false;
    }

    public float getAccuracy() {
        return TT.getAccuracy();
    }

    private void Update()
    {
        if(overTarget && getAccuracy() != 100 && getAccuracy() <= 100)
        {
            TT.changeAccuracy(0.2f);
            accuracyNumber.text =  Mathf.Floor(getAccuracy()) + "%";
        }
        else if(!overTarget && getAccuracy() != 0 && getAccuracy() > 0)
        {
            TT.changeAccuracy(-0.2f);
            accuracyNumber.text = Mathf.Floor(getAccuracy()) + "%";
        }
        float scale = 1 - (getAccuracy()/110);
        target.transform.localScale = new Vector3(scale, scale, 1);
    }
}
