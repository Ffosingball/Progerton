using UnityEngine;

public static class GameInfo
{
    public static int currentLevel;


    public static float soundEffectsVolume;
    public static float musicVolume;
    public static int languageIndex;
    public static int uiSize;
    public static bool showPrompts;


    public static void setPreferences(SettingsPreferences settingsPreferences)
    {
        soundEffectsVolume = settingsPreferences.soundEffectsVolume;
        musicVolume = settingsPreferences.musicVolume;
        languageIndex = settingsPreferences.languageIndex;
        uiSize = settingsPreferences.uiSize;
        showPrompts = settingsPreferences.showPrompts;
    }
}