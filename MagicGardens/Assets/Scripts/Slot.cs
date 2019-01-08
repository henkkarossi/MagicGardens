using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    //Used in ItemList
    public void CreateItem()
    {

        int rightIndex = ItemList.instance.itemSlots.FindIndex(x => x == gameObject);

        if (ItemList.instance.ownedItems[rightIndex].amount > 0)
        {
            GameObject newItem = Instantiate(ItemList.instance.ownedItems[rightIndex].item);
            newItem.transform.position = GameManager.instance.gardenArea.transform.position;
            newItem.transform.SetParent(GameObject.Find("GardenThingsList").transform);

            //decrease item amount
            ItemList.GardenItem currentObject = ItemList.instance.ownedItems[rightIndex];
            currentObject.amount = ItemList.instance.ownedItems[rightIndex].amount - 1;
            ItemList.instance.ownedItems[rightIndex] = currentObject;

            GameManager.instance.ToGarden();
        }
       
        if(ItemList.instance.ownedItems[rightIndex].amount == 0)
        {
            gameObject.GetComponent<Image>().color = Color.black;
        }
    }


    //Used in Shop
    public void BuyItem()
    {
        if (GetPrice() <= GameManager.instance.happiness)
        {
            if (ItemList.instance.ownedItems.FindIndex(x => x.item.GetComponent<Image>().sprite == gameObject.GetComponent<Image>().sprite) == -1)
            {
                int index = SaveManager.instance.gardenItemPrefabs.FindIndex(x => x.GetComponent<Image>().sprite == gameObject.GetComponent<Image>().sprite);
                ItemList.GardenItem newItem = new ItemList.GardenItem(SaveManager.instance.gardenItemPrefabs[index], 1);
                ItemList.instance.ownedItems.Add(newItem);
            }
            else
            {
                int i = ItemList.instance.ownedItems.FindIndex(x => x.item.GetComponent<Image>().sprite == gameObject.GetComponent<Image>().sprite);
                ItemList.instance.ownedItems[i] = new ItemList.GardenItem(ItemList.instance.ownedItems[i].item, ItemList.instance.ownedItems[i].amount + 1);
            }
            GameManager.instance.happiness -= GetPrice();
        }
    }

   int GetPrice()
    {
        int priceInt;

        Text price = gameObject.GetComponentInChildren<Text>();
        if (price != null)
        {
            return priceInt = int.Parse(price.text);
        }

        return 0;
    }
}
