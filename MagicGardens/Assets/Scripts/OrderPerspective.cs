using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPerspective : MonoBehaviour
{

    static OrderPerspective instance;

    public List<Canvas> allCanvases;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update ()
    {


        foreach (GameObject theObject in GameObject.FindGameObjectsWithTag("OrderPerspective"))
        {

            if (theObject.transform.position.y < GameManager.instance.gardenAreaRT.position.y + GameManager.instance.gardenAreaRT.sizeDelta.y * 0.75f)
            {
                if (!theObject.GetComponent<Canvas>())
                {
                    allCanvases.Add(theObject.AddComponent<Canvas>());
                    Canvas canvas = theObject.GetComponent<Canvas>();
                    canvas.overrideSorting = true;
                }

                if (theObject.GetComponent<Canvas>() != null)
                {
                    Canvas canvas = theObject.GetComponent<Canvas>();

                    if (canvas.overrideSorting)
                    {
                        canvas.sortingOrder = Mathf.RoundToInt((GameManager.instance.gardenAreaRT.position.y
                                                                + GameManager.instance.gardenAreaRT.sizeDelta.y * 0.75f) - theObject.transform.position.y);

                    }
                }
            }
        }
    }

}
