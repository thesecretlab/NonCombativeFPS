using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretCalibration : MonoBehaviour {
   
    float accuracy = 0;
    float fillRate = (float)0.5;
    float empRate = (float)0.2;
    Text accuracyText;
    float cWidth;
    float cHeight;
    
    float randomX;
    float randomY;
    float change;
    float moveSpeed;
    bool overTarget;
    Image target;
    RectTransform canvas;
    
    // Use this for initialization
    void Awake()
    {
        canvas = gameObject.GetComponent<RectTransform>();
        cWidth = canvas.rect.width;
        cHeight = canvas.rect.height;
        target = GameObject.Find("Target").GetComponent<Image>();
        accuracyText = GameObject.Find("AccuracyN").GetComponent<Text>();
       

        
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
        moveSpeed = 10 + (accuracy * (float)0.10);
        if(Time.time >= change)
        {
            randomX = Random.Range((float)-10.0, (float)10.0);// Random float is returned and used to update sprite position.
            randomY = Random.Range((float)-10.0, (float)10.0);
            //Used to change the direction of the sprite.
            change = Time.time + Random.Range((float)0.5, (float)1.5);
        }
        target.rectTransform.Translate((randomX * moveSpeed * Time.deltaTime), (randomY * moveSpeed * Time.deltaTime), 0);
    
       
        //Used if the sprite reaches a left or right border.
        if(target.rectTransform.position.x >= (cWidth / 2) || target.rectTransform.position.x <= (cWidth /2) - cWidth)
        {
            randomX =- randomX;
            
         
        }
        //Used if the Sprite reaches a upper or lower border.
        if (target.rectTransform.position.y >= (cHeight / 2) || target.rectTransform.position.y <= (cHeight / 2) - cHeight)
        {
            randomY =- randomY;
            
            
        }
        
    }

    private void LateUpdate()
    {
        if(overTarget && accuracy != 100)
        {

            target.rectTransform.sizeDelta -= new Vector2(0.25f, 0.25f);
            //scale.localScale -= new Vector3(1F, 1F, 1F);
            accuracy += fillRate;
            accuracyText.text = accuracy.ToString();

        }
        else if(!overTarget && accuracy != 0)
        {
            target.rectTransform.sizeDelta += new Vector2(0.25f, 0.25f);
            accuracy -= fillRate;
            accuracyText.text = accuracy.ToString();
        }
    }
    
    
}
