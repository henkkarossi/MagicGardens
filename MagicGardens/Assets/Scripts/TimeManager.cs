using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;

    [System.Serializable]
    public struct GameTime
    {
        public int minute;
        public int hour;
        public int day;
        public int month;
        public int year;

        public GameTime(int minute, int hour, int day, int month, int year)
        {
            this.minute = minute;
            this.hour = hour;
            this.day = day;
            this.month = month;
            this.year = year;
        }
    }
    public GameTime thisGameTime;
    public GameTime lastGameTime;
    public GameTime timePassed;
    [Space]
    public int timePassedInMinutes;
    


    private void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SaveManager.instance.LoadGame();
        lastGameTime = thisGameTime;
        CheckTime();

        timePassed = new GameTime(thisGameTime.minute - lastGameTime.minute,
                                  thisGameTime.hour - lastGameTime.hour,
                                  thisGameTime.day - lastGameTime.day,
                                  thisGameTime.month - lastGameTime.month,
                                  thisGameTime.year - lastGameTime.year);

        //approximately
        timePassedInMinutes = timePassed.minute
                                        + timePassed.hour * 60
                                        + timePassed.day * 60 * 24
                                        + timePassed.month * 60 * 24 * 30
                                        + timePassed.year * 60 * 24 * 30 * 365;
    }

    // Update is called once per frame
    void Update () {

        CheckTime();

        //ApplicationPause ja timemanager dont destroy on load ekaan sceneen, 

    }

    void CheckTime()
    {
        thisGameTime = new GameTime(System.DateTime.Now.Minute, 
                                    System.DateTime.Now.Hour, 
                                    System.DateTime.Now.Day, 
                                    System.DateTime.Now.Month, 
                                    System.DateTime.Now.Year);
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        SaveManager.instance.SaveGame();
    }

    private void OnApplicationQuit()
    {
        print("saveBeforeQuit");
        SaveManager.instance.SaveGame();
    }
}
