using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public bool moving = true;
    Button button;

    public GameObject itemMenu;
    [HideInInspector]
    public bool hasItemMenu;
    bool beingBorn;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        DisableUnicorns(false);

    }

    private void Update()
    {
      
        if(moving)
        {
            button.enabled = false;
            ItemList.instance.addState = ItemList.AddMenuState.moving;
            Moving();
        }
        else
        {
            ItemList.instance.addState = ItemList.AddMenuState.idle;
            button.enabled = true;
        }


    }

    private void LateUpdate()
    {
        //update needed things after SaveSpawn one time
        if (!beingBorn)
        {
            beingBorn = true;
            ManageSize();
            //add to list and name it
            ItemList.instance.itemsInScene.Add(gameObject);
            gameObject.name = gameObject.name + ItemList.instance.itemsInScene.FindIndex(x => x == gameObject);
            PatternRecogniton.instance.CheckForPatterns(gameObject);
        }

    }

    public void StartMoving()
    {
        moving = true;
        DisableUnicorns(false);
    }

    public void DestroyItem()
    {

        int i = ItemList.instance.ownedItems.FindIndex(x => x.item.name.Substring(0, 1) == gameObject.name.Substring(0, 1));
        ItemList.GardenItem itemUpdated = new ItemList.GardenItem( ItemList.instance.ownedItems[i].item, ItemList.instance.ownedItems[i].amount + 1);
        ItemList.instance.ownedItems[i] = itemUpdated;
        ItemList.instance.itemsInScene.Remove(gameObject);
        Destroy(gameObject);
    }

    void Moving()
    {
        

        if (InputManager.instance.cursorPosition.y < GameManager.instance.border.transform.position.y - 0.01f)
        {
            transform.position = InputManager.instance.cursorPosition;
            ManageSize();
        }
        else
        {

            //print(transform.position);
            StopMoving();
        }

        if (!Application.isEditor)
        {
            if (InputManager.instance.clickState == InputManager.ClickStates.release)
            {
                print(transform.position);
                StopMoving();
            }
        }
        else
        {
            if (InputManager.instance.clickState == InputManager.ClickStates.press)
            {
                print(transform.position);
                StopMoving();
            }
        }

    }

    public void ManageSize()
    {
        if (gameObject.GetComponentInChildren<Canvas>())
        {
            GameObject targetObject = gameObject.GetComponentInChildren<Canvas>().gameObject;
            Canvas canvas = targetObject.GetComponent<Canvas>();
            RectTransform rectTransform = targetObject.GetComponent<RectTransform>();

            if (canvas != null)
            {
                float sortingModified = 80 + canvas.sortingOrder * 0.1f;
                rectTransform.sizeDelta = new Vector2(sortingModified, sortingModified);
            }
        }
    }

    void StopMoving()
    {
        FixPos();
        //check if item is in pattern. Script will reward player after
        PatternRecogniton.instance.CheckForPatterns(gameObject);
        moving = false;
        DisableUnicorns(true);
    }

    public void DisableUnicorns(bool onOff)
    {
        foreach(GameObject unicorn in GameObject.FindGameObjectsWithTag("OrderPerspective"))
        {

            if (unicorn.name.Contains("Unicorn"))
            {
                unicorn.GetComponent<Image>().enabled = onOff;
            }
        }
    }

    public void SpawnItemMenu()
    {
        if (!hasItemMenu)
        {
            hasItemMenu = true;
            GameObject newItemMenu = Instantiate(itemMenu);
            newItemMenu.transform.parent = GameObject.Find("UICanvas").transform;
            newItemMenu.GetComponent<ItemMenu>().owner = gameObject;
            newItemMenu.transform.position = InputManager.instance.cursorPosition;
        }
    }


    void FixPos()
    {
        float gardenBorderX = GameManager.instance.gardenAreaRT.transform.position.x + GameManager.instance.gardenAreaRT.sizeDelta.x * 0.2f;
        float gardenBorderXnegative = GameManager.instance.gardenAreaRT.transform.position.x - GameManager.instance.gardenAreaRT.sizeDelta.x * 0.2f;

        float gardenBorderY = GameManager.instance.gardenAreaRT.transform.position.y + GameManager.instance.gardenAreaRT.sizeDelta.y * 0.2f;
        float gardenBorderYnegative = GameManager.instance.gardenAreaRT.transform.position.y - GameManager.instance.gardenAreaRT.sizeDelta.y * 0.2f;

        Vector2 newPos = transform.position;

        if(InputManager.instance.cursorPosition.x > gardenBorderX 
           && InputManager.instance.cursorPosition.x < gardenBorderXnegative
           &&InputManager.instance.cursorPosition.y > gardenBorderY
           &&InputManager.instance.cursorPosition.y < gardenBorderYnegative)
        {
            if (InputManager.instance.cursorPosition.x > gardenBorderX)
            {
                newPos.x = gardenBorderX;
            }
            else if (InputManager.instance.cursorPosition.x < gardenBorderXnegative)
            {
                newPos.x = gardenBorderXnegative;
            }
            if (InputManager.instance.cursorPosition.y > gardenBorderY)
            {
                newPos.y = gardenBorderY;
            }
            else if (InputManager.instance.cursorPosition.y < gardenBorderYnegative)
            {
                newPos.y = gardenBorderYnegative;
            } 
        }

        transform.position = newPos;
    }
}
