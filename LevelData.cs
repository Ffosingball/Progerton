using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LevelData
{
    public List<string> levelNames, sceneName;
    public List<bool> locked;
    public List<float> bestTime;
    public List<int> pictureRef;

    public LevelData()
    {
        Debug.Log("Created!");
    }
}
