using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SettingsPreferences
{
    public float soundEffectsVolume;
    public float musicVolume;
    public int languageIndex;
    public int uiSize;
    public bool showPrompts;

    public SettingsPreferences()
    {
        Debug.Log("Created!");
    }
}