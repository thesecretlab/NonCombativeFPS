using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretCalibration : MonoBehaviour {
   
    float accuracy = 0;
    Text accuracyText;
    float cWidth;
    float cHeight;
    Vector3 scale;
    float randomX;
    float randomY;
    float change;
    float moveSpeed;
    bool overTarget;
    Sprite target;
    private Rigidbody2D rb2d;
    
    // Use this for initialization
    void Awake()
    {
       
        target = GameObject.Find("cirTarget").GetComponent<Sprite>();
        rb2d = GameObject.Find("cirTarget").GetComponent<Rigidbody2D>();
        accuracyText = GameObject.Find("AccuracyN").GetComponent<Text>();
       

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colliding");


        BounceTarget();



    }

    private void OnMouseEnter()
    {
        setOverTarget();
    }

    private void OnMouseExit()
    {


        setLostTarget();
        
            
        
    }
    /*
    private void OnMouseOver()
    {
        Debug.Log("enter");
        if (accuracy != 100.0)
        {
            transform.localScale -= new Vector3(0.35F, 0.35F, 0.35F);
            accuracy += fillRate;
            accuracyText.text = accuracy.ToString();
        }
        else
        {
            Debug.Log("Maximum Accuracy Achieved");
        }
    }
    */
    

    public void setOverTarget()
    {
        overTarget = true;
    }

    public void setLostTarget()
    {
        overTarget = false;
    }

    // Update is called once per frame
    void Update () {
        moveSpeed = 60 + (accuracy * 0.01f);
        if(Time.time >= change)
        {
            randomX = Random.Range((float)-10.0, (float)10.0);// Random float is returned and used to update sprite position.
            randomY = Random.Range((float)-10.0, (float)10.0);
            //Used to change the direction of the sprite.
            change = Time.time + Random.Range((float)0.5, (float)1.5);
        }
        //transform.Translate((randomX * moveSpeed), (randomY * moveSpeed), 0);
        rb2d.AddForce(new Vector2((randomX * moveSpeed), (randomY * moveSpeed)));

    }

    void BounceTarget()
    {
        float random = Random.Range(0, 2);
        if (random < 1)
        {
            rb2d.AddForce(new Vector2(20, -15));
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }
    private void LateUpdate()
    {
        if(overTarget && accuracy != 100 && accuracy <= 100)
        {

            //target.rectTransform.sizeDelta -= new Vector2(0.25f, 0.25f);
            transform.localScale -= new Vector3(0.2F, 0.2F, 0.2F);
            accuracy += 0.2f;
            accuracyText.text =  Mathf.Floor(accuracy) + "%";
        }
        else if(!overTarget && accuracy != 0 && accuracy > 0)
        {
           
            //target.rectTransform.sizeDelta += new Vector2(0.25f, 0.25f);
            transform.localScale += new Vector3(0.2F, 0.2F, 0.2F);
            accuracy -= 0.2f;
            accuracyText.text = Mathf.Floor(accuracy) + "%";
        }
    }
    
    
}
