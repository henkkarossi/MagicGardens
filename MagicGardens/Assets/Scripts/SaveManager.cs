using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;
    public bool deleteSaveAtStart;
    public bool doNotSave;

    public List<GameObject> gardenItemPrefabs;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        if(deleteSaveAtStart)
        {
            DeleteSavedGame();
        }
    }

    public void SaveGame()
    {
        if(!doNotSave)
        {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");
        SaveData data = new SaveData();

        //put here things you want to save
        data.gameTime = TimeManager.instance.thisGameTime;
        data.happiness = GameManager.instance.happiness;

        //Amount of items player owns
        List<int> temporaryList = new List<int>();
        foreach (ItemList.GardenItem item in ItemList.instance.ownedItems)
        {
            temporaryList.Add(item.amount);
        }
        data.ownedItemsAmounts = new List<int>(temporaryList);

        //Objects and Their Locations
        foreach (Transform child in GameObject.Find("GardenThingsList").transform)
        {
            GameObject theThing = child.gameObject;
            SavedObject theSavedObject = new SavedObject(); 
            theSavedObject.type = gardenItemPrefabs.FindIndex(x => x.name.Substring(0,1) == theThing.name.Substring(0,1));
            theSavedObject.positionX = theThing.transform.position.x;
            theSavedObject.positionY = theThing.transform.position.y;

            //new
            data.gardenItems.Add(theSavedObject);
   
        }
     
        bf.Serialize(file, data);
            file.Close();
        }
    }

    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            //put here things you want to load
            TimeManager.instance.thisGameTime = data.gameTime;
            GameManager.instance.happiness = data.happiness;
            GameManager.instance.AddPastHappiness();

            foreach (int itemAmount in data.ownedItemsAmounts)
            {
                int i = data.ownedItemsAmounts.FindIndex(x => x == itemAmount);
                ItemList.instance.ownedItems[i] = new ItemList.GardenItem( ItemList.instance.ownedItems[i].item, itemAmount);
            }
    
            if (data.gardenItems.Count != 0)
            {
                //Load Objects and Their Locations
                foreach (SavedObject savedObject in data.gardenItems)
                {
                    //print(savedObject.type);
                    GameObject spawnedThing = Instantiate(gardenItemPrefabs[savedObject.type]);
                    spawnedThing.transform.SetParent(GameObject.Find("GardenThingsList").transform);
                    spawnedThing.transform.position = new Vector2((float)savedObject.positionX, (float)savedObject.positionY);

                    if(spawnedThing.GetComponent<Item>())
                    {
                        Item item = spawnedThing.GetComponent<Item>();
                        item.moving = false;
                        item.DisableUnicorns(true);
                    }
                }
            }


        }
        else
        {
            print("noFile");
        }
    }

    public void DeleteSavedGame()
    {

        if (File.Exists(Application.persistentDataPath + "/savedData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/savedData.dat");
        }

    }
}

[System.Serializable]
public class SaveData
{
    //put here what you want to save
    public TimeManager.GameTime gameTime;
    public int happiness;
    public List<int> ownedItemsAmounts; 

    public List<SavedObject> gardenItems;

    public SaveData()
    {
        gardenItems = new List<SavedObject>();
    }


}

[System.Serializable]
public struct SavedObject
{
    public int type;
    public float positionX;
    public float positionY;

    public SavedObject(int type, float positionX, float positionY)
    {
        this.type = type;
        this.positionX = positionX;
        this.positionY = positionY;
    }

}
