using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternRecogniton : MonoBehaviour {

    public static PatternRecogniton instance;

    //public List<GameObject> allItems = new List<GameObject>();

    public int lowTierBonus, highTierBonus;
    [HideInInspector]
    public int lowTierHits, highTierHits;


    [Range(0,100)]
    public float patternRange = 25;
 
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

    public void CheckForPatterns(GameObject theItem)
    {

        if (ItemList.instance.itemsInScene.Count > 1)
        {

            List<GameObject> temporaryList = new List<GameObject>(ItemList.instance.itemsInScene);

            foreach (GameObject item in ItemList.instance.itemsInScene)
            {
                temporaryList.Remove(item);

                foreach (GameObject comparedItem in temporaryList)
                {

                    if (theItem.transform.position.x > comparedItem.transform.position.x - patternRange
                       && theItem.transform.position.x < comparedItem.transform.position.x + patternRange)
                    {
                        WhatBonus(theItem, comparedItem);
                    }

                    if (theItem.transform.position.y > comparedItem.transform.position.y - patternRange
                        && theItem.transform.position.y < comparedItem.transform.position.y + patternRange)
                    {
                        lowTierHits++;
                    }


                }

                temporaryList.Add(item);
            }

            lowTierBonus = lowTierHits;
            highTierBonus = highTierHits;
            lowTierHits = 0;
            highTierBonus = 0;

        }
        else
        {
            print("no other items");
        }

    }

    void WhatBonus(GameObject theItem, GameObject comparedItem)
    {
        Image theItemImage = theItem.GetComponentInChildren<Image>();
        Image comparedItemImage = comparedItem.GetComponentInChildren<Image>();

        //print(theItemImage.sprite + " : " + comparedItemImage.sprite);

        if (theItemImage.sprite == comparedItemImage.sprite)
        {
            highTierHits++;
        }
        else
        {
            lowTierHits++;
        }

    }

}
