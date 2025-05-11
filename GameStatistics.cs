using System;

[System.Serializable]
public class GameStatistics
{
    public int bridgesActivated=0;
    public int gatesOpened=0;
    public int platformsActivated=0;
    public int numOfReplaysMade=0;
    public float shortestTime=99999;
    public float longestTime=0;
    public int shortestTimeAtLevel=-1;
    public int longestTimeAtLevel=-1;
    public float distanceWalked=0;
    public float distanceFlew=0;
    public float timeSpentInGame=0;


    /*public string ToString()
    {
        return "Sound volume: "+soundEffectsVolume+"; music volume: "+musicVolume+"; prompts: "+showPrompts+"; warnings: "+showWarningsScreen+"; sensitivity: "+sensitivity+"; language: "+languageIndex;
    }*/
}