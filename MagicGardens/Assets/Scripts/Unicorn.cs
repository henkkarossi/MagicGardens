using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unicorn : MonoBehaviour {

    float timer;
    float moveRate;
    public Vector3 randomPos;
    float randomSpeed;
    float xDirection;
    int direction;
    public bool stop;

	void Start () {

        StartCoroutine("StopMovement");

    }
	
	void Update () {

        float gardenBorderX = GameManager.instance.gardenAreaRT.transform.position.x + GameManager.instance.gardenAreaRT.sizeDelta.x * 0.5f;
        float gardenBorderXnegative = GameManager.instance.gardenAreaRT.transform.position.x - GameManager.instance.gardenAreaRT.sizeDelta.x * 0.5f;

        float gardenBorderY = GameManager.instance.gardenAreaRT.transform.position.y + GameManager.instance.gardenAreaRT.sizeDelta.y * 0.5f;
        float gardenBorderYnegative = GameManager.instance.gardenAreaRT.transform.position.y - GameManager.instance.gardenAreaRT.sizeDelta.y * 0.5f;

       
        if (stop)
        {
            
            randomPos = new Vector3(Random.Range(gardenBorderXnegative, gardenBorderX), Random.Range(gardenBorderYnegative, gardenBorderY), 0);
            randomSpeed = Random.Range(30, 60);
            timer = Time.time;
            moveRate = Random.Range(1, 6);
        }
        else 
        {
            
                float step = randomSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, randomPos, step);

                if (xDirection < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
                xDirection = transform.position.x;

            transform.rotation = Quaternion.Euler(0, 0, 5 * Mathf.Sin(Time.time * 8));
        }

        ManageSize();
    }

    public void ManageSize()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        if (canvas != null)
        {
            float sortingModified = 80 + canvas.sortingOrder * 0.1f;
            sortingModified = sortingModified * 0.015f;
            rectTransform.localScale = new Vector2(sortingModified * direction, sortingModified);
        }
    }

    private IEnumerator StopMovement()
    {
        stop = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 2));
        stop = false;
        yield return new WaitForSeconds(Random.Range(2f, 6));
        StartCoroutine("StopMovement");
    }


    
}
