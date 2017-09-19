using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretCalibration : MonoBehaviour {
    Sprite target;
    float accuracy = 0;
    float fillRate = (float)0.5;
    float empRate = (float)0.2;
    Text accuracyText;
    float maxY = 90;
    float minY = -90;
    float maxX = 400;
    float minX = -400;
    float randomX;
    float randomY;
    float change;
    float moveSpeed;
    int OutofBorderX;
    int OutofBorderY;
    Vector3 start = new Vector3(0, 0, 0);
	// Use this for initialization
	void Awake () {
        target = GameObject.Find("Target").GetComponent<Sprite>();
        accuracyText = GameObject.Find("AccuracyN").GetComponent<Text>();
        OutofBorderX = 0;
        OutofBorderY = 0;
    }
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
    // Update is called once per frame
    void Update () {
        moveSpeed = 10 + (accuracy * (float)0.10);
        if(Time.time >= change)
        {
            randomX = Random.Range((float)-2.0, (float)2.0);// Random float is returned and used to update sprite position.
            randomY = Random.Range((float)-2.0, (float)2.0);
            //Used to change the direction of the sprite.
            change = Time.time + Random.Range((float)0.5, (float)1.5);
        }
        transform.Translate((randomX * moveSpeed * Time.deltaTime), (randomY * moveSpeed * Time.deltaTime), 0);
        //Used if the sprite reaches a left or right border.
        if(transform.position.x >= maxX || transform.position.x <= minX)
        {
            randomX =- randomX;
            OutofBorderX++;
            if (OutofBorderX > 10)
            {
                transform.position = start;
            }
        }
        //Used if the Sprite reaches a upper or lower border.
        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            randomY = -randomY;
            OutofBorderY++;
            if (OutofBorderY > 10)
            {
                transform.position = start;
            }
        }
    }
    /*
    private void LateUpdate()
    {
        if (accuracy < 0)
        {
            accuracy = 0;
        }
        if (accuracy != 0)
        {
            accuracy -= empRate;
        }
    }
    */
}
