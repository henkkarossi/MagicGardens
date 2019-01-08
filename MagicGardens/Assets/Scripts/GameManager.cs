using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public enum GameState { garden, itemList, shop}
    public GameState gameState;


    [HideInInspector]
    public ItemList viewingItemList;
    [HideInInspector]
    public Shop shop;

    public GameObject mainBase, itemListBase, shoppingBase, gardenArea;
    [HideInInspector]
    public RectTransform gardenAreaRT;

    public int happiness;
    public Text happinessMeter;

    public GameObject unicorn;
    public GameObject unicornPlusSymbol;
    public GameObject uiArea;
    public GameObject border;
    

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


        viewingItemList = gameObject.GetComponent<ItemList>();
        shop = gameObject.GetComponent<Shop>();

        gardenAreaRT = gardenArea.GetComponent<RectTransform>();

    }

    private void Start()
    {

        StartCoroutine(AddHappiness());
    }

    private void Update()
    {
        happinessMeter.text = happiness.ToString();


        switch (gameState)
        {

            case GameState.garden:             

                break;

            case GameState.itemList:
                viewingItemList.ViewItemList();
                break;

            case GameState.shop:
                shop.Shopping();
                break;
        }
    }

    public void ToGarden()
    {
        gameState = GameState.garden;

        mainBase.SetActive(true);
        shoppingBase.SetActive(false);
        itemListBase.SetActive(false);
    }
    public void ToShop()
    {
        gameState = GameState.shop;

        mainBase.SetActive(false);
        shoppingBase.SetActive(true);
        itemListBase.SetActive(false);
    }
    public void ToItemList()
    {
        gameState = GameState.itemList;

        mainBase.SetActive(false);
        shoppingBase.SetActive(false);
        itemListBase.SetActive(true);
    }


    IEnumerator AddHappiness()
    {
        int unicornAmount = GameObject.FindObjectsOfType(typeof(Unicorn)).Length;
        happiness += unicornAmount;
        yield return new WaitForSeconds(5);

        MoreUnicorns(unicornAmount);

        StartCoroutine(AddHappiness());
    }

    public void AddPastHappiness()
    {
        int unicornAmount = GameObject.FindObjectsOfType(typeof(Unicorn)).Length;
        //print(unicornAmount);
        //print(TimeManager.instance.timePassedInMinutes);

        happiness += unicornAmount * 12 * TimeManager.instance.timePassedInMinutes;
        

        MoreUnicorns(unicornAmount);
    }

    void MoreUnicorns(int unicornAmount)
    {
        int patternsEqualsUnicorns = PatternRecogniton.instance.lowTierBonus + PatternRecogniton.instance.highTierBonus * 2;

        if(unicornAmount < patternsEqualsUnicorns * 0.75f)
        {
            print("spawnNew");
            GameObject spawnedUnicorn = Instantiate(unicorn);
            spawnedUnicorn.transform.SetParent(GameObject.Find("GardenThingsList").transform);
            //spawnedUnicorn.transform.position = transform.parent.transform.position;
            GameObject newSymbol = Instantiate(unicornPlusSymbol);
            newSymbol.transform.SetParent(uiArea.transform);
            Destroy(newSymbol, 2); 
        } 
    }

   
}
