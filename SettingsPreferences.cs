using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SettingsPreferences
{
    public float soundEffectsVolume;
    public float musicVolume;
    public float sensitivity;
    public int languageIndex;
    public bool showPrompts;
    public bool showWarningsScreen;
    public string[] keyBindings;

    public SettingsPreferences()
    {
        soundEffectsVolume = 0;
    }

    public string ToString()
    {
        return "Sound volume: "+soundEffectsVolume+"; music volume: "+musicVolume+"; prompts: "+showPrompts+"; warnings: "+showWarningsScreen+"; sensitivity: "+sensitivity+"; language: "+languageIndex;
    }
}