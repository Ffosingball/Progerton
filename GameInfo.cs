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

        if(time>gameStatistics.longestTime)
        {
            gameStatistics.longestTime = time;
            gameStatistics.longestTimeAtLevel = currentLevel;
        }
    }
}