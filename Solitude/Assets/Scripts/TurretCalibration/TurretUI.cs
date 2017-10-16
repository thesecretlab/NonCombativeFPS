using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretUI : MonoBehaviour {
    
	///Declaraion of the target that player will be aiming for.
	public Target target;
    ///Declaration of the Turret Terminal object.
    TurretTerminal TT;
	
	///Variable Declaration
	public Text accuracyNumber;
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

	///Declaring a rigid body for use on the target, so it will stay in borders.
    private Rigidbody2D rb2d;
    
    /// Use this for initialization
	///Awake is executed when the script is first used
	///Grabs all the UI elements and stores them in variables.
    void Awake()
    {
        rb2d = target.GetComponent<Rigidbody2D>();
        width = GetComponentInParent<RectTransform>().rect.width;
        height = GetComponentInParent<RectTransform>().rect.height;
        target.setUI(this);
    }
    
	/// Setter for the Turret Terminal.
	public void setTerminal(TurretTerminal tt) {
        TT = tt;
    }
	
	///Triggers when the mouse is over the target.
    public void setOverTarget()
    {
        overTarget = true;
    }
	///Triggers when the mouse has left the target.
    public void setLostTarget()
    {
        overTarget = false;
    }

	///Gets the current turret accuracy.
    public float getAccuracy() {
        return TT.getAccuracy();
    }

    private void Update()
    {
		///if the mouse is over the target and the accuracy is less then 100, increase the accuracy.
        if(overTarget && getAccuracy() != 100 && getAccuracy() <= 100)
        {
            TT.changeAccuracy(0.2f);
            accuracyNumber.text =  Mathf.Floor(getAccuracy()) + "%";
        }
		///If the target is not over the target and the accuracy is greater the 0, decrease the accuracy.
        else if(!overTarget && getAccuracy() != 0 && getAccuracy() > 0)
        {
            TT.changeAccuracy(-0.2f);
            accuracyNumber.text = Mathf.Floor(getAccuracy()) + "%";
        }
		///Scales the target bigger as the accuracy is getting lower and decreasing its size as the accuracy increases.
        float scale = 1 - (getAccuracy()/110);
        target.transform.localScale = new Vector3(scale, scale, 1);
    }
}
