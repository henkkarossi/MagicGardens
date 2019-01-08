using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour {

    public static ItemList instance;

    public enum AddMenuState { idle, moving}
    public AddMenuState addState;

    public List<GameObject> allDifferentItems;

    [System.Serializable]
    public struct GardenItem
    {
        public GameObject item;
        public int amount;

        public GardenItem(GameObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
        public GardenItem(GameObject item)
        {
            this.item = item;
            amount = 0;
        }
    }

    public List<GardenItem> ownedItems;
    public List<GameObject> itemsInScene;
    public List<GameObject> itemSlots;
    public List<Image> itemSlotImages;
    public List<Button> itemSlotButtons;

    public GameObject listBase;
    RectTransform listBaseRT;
    public GameObject slot;

    public GameObject scrollbar;





    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        listBaseRT = listBase.GetComponent<RectTransform>();

      
        for (int i = 0; i < ownedItems.Count; i++)
        {

            ownedItems[i].item.name = "" + i;
        }

    }


    public void ViewItemList()
    {

        float slotSize = slot.GetComponent<RectTransform>().sizeDelta.y;

        listBaseRT.sizeDelta = new Vector2(slotSize, ownedItems.Capacity * slotSize);



        if (itemSlots.Count < ownedItems.Count)
        {


            for (int i = itemSlots.Count; i < ownedItems.Count; i++)
            {

                slot.GetComponent<Image>().sprite = ownedItems[i].item.GetComponent<Image>().sprite;
                GameObject newSlot = Instantiate(slot, listBase.transform);
                newSlot.transform.parent = listBase.transform;

                itemSlots.Add(newSlot);
                itemSlots[i].name = "" + i;


                Image newSlotImage = itemSlots[i].GetComponent<Image>();
                itemSlotImages.Add(newSlotImage);
                
                Button newSlotButton = itemSlots[i].GetComponent<Button>();
                itemSlotButtons.Add(newSlotButton);

                itemSlotImages[i].sprite = ownedItems[i].item.GetComponent<Image>().sprite;


            }

        }


  
        if(itemSlots.Count > ownedItems.Count)
        {
            for (int i = itemSlots.Count -1 ; i >= ownedItems.Count; i--)
            {
                Destroy(itemSlots[i]);
                itemSlots.Remove(itemSlots[i]);
            }
        }


        if (itemSlots.Count <= 3)
        {
            scrollbar.SetActive(false);
        }
        else
        {
            scrollbar.SetActive(true);
        }



    }







    
}
