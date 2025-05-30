using System;
using Unity.Mathematics;
using UnityEngine;


public static class GameInfo
{
    public static int currentLevel=-1;
    public static bool musicIsMuffled = false;
    public static GameStatistics gameStatistics = null;
    public static OtherGameInfo otherGameInfo=null;


    public static void getStatistics()
    {
        gameStatistics = SaveSystem.LoadGameData();

        if (gameStatistics == null)
            gameStatistics = new GameStatistics();
    }


    public static void getOtherInfo()
    {
        otherGameInfo = SaveSystem.LoadOtherGameData();

        if (otherGameInfo == null)
            otherGameInfo = new OtherGameInfo();
    }


    public static void SaveData()
    {
        SaveSystem.SaveGameData(gameStatistics);
    }


    public static void SaveGameInfo()
    {
        SaveSystem.SaveOtherGameData(otherGameInfo);
    }


    public static void setTime(float time)
    {
        if (time < gameStatistics.shortestTime)
        {
            gameStatistics.shortestTime = time;
            gameStatistics.shortestTimeAtLevel = currentLevel;
        }

        float maxTime = time;
        int atLevel = currentLevel;
        //Debug.Log("curTime "+maxTime+"; level "+atLevel);
        LevelData data = SaveSystem.LoadLevelData();
        for (int i = 0; i < data.locked.Count; i++)
        {
            if (!data.locked[i] && i != currentLevel)
            {
                //Debug.Log("i "+i+"; time "+data.bestTime[i]);
                if (maxTime < data.bestTime[i] && data.bestTime[i] != 99999)
                {
                    maxTime = data.bestTime[i];
                    atLevel = i;
                }
            }
        }

        gameStatistics.longestTime = maxTime;
        gameStatistics.longestTimeAtLevel = atLevel;
    }


    public static string convertDistance(float distance)
    {
        if(distance>1000)
            return Math.Round((double)(distance/1000))+" km "+Math.Round((double)(distance%1000)) +" m";
        else
            return Math.Round((double)distance)+" m";
    }


    public static string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(gameStatistics.timeSpentInGame / 3600f);
        int hoursRemainder = Mathf.FloorToInt(gameStatistics.timeSpentInGame % 3600f);
        int minutes = Mathf.FloorToInt(hoursRemainder / 60f);
        int seconds = Mathf.FloorToInt(hoursRemainder % 60f);
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}