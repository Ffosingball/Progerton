using System;
using Unity.Mathematics;
using UnityEngine;


public static class GameInfo
{
    public static int currentLevel=0;
    public static GameStatistics gameStatistics=null;


    public static void getStatistics()
    {
        gameStatistics = SaveSystem.LoadGameData();
        if(gameStatistics==null)
            gameStatistics = new GameStatistics();
    }


    public static void SaveData()
    {
        SaveSystem.SaveGameData(gameStatistics);
    }


    public static void setTime(float time)
    {
        if(time<gameStatistics.shortestTime)
        {
            gameStatistics.shortestTime = time;
            gameStatistics.shortestTimeAtLevel = currentLevel;
        }

        float maxTime = time;
        int atLevel = currentLevel;
        //Debug.Log("curTime "+maxTime+"; level "+atLevel);
        LevelData data = SaveSystem.LoadLevelData();
        for(int i=0; i<data.locked.Count; i++)
        {
            if(!data.locked[i] && i!=currentLevel)
            {
                //Debug.Log("i "+i+"; time "+data.bestTime[i]);
                if(maxTime<data.bestTime[i] && data.bestTime[i]!=99999)
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
}