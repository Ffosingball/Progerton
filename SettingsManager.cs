using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

/*This class manages all ui changes in main menu*/

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject keyBindingScreen;
    [SerializeField]
    private GameObject otherSettingsScreen;
    /*[SerializeField]
    private Button otherSettingsBut;
    [SerializeField]
    private Button keyBindingBut;*/
    [SerializeField]
    private Slider sliderMusic, sliderSound;
    [SerializeField]
    private TMP_Text textSliderMusic, textSliderSound;
    [SerializeField]
    private Dropdown languageChoice;
    [SerializeField]
    private Toggle toggleShowPrompts;


    private SettingsPreferences settingsPreferences;
    private bool isInitialized = false;


    private void Start()
    {
        SwitchToOtherSettings();

        settingsPreferences = SaveSystem.LoadSettingsPreferences();
        if(settingsPreferences==null)
            settingsPreferences = createPreferences();

        GameInfo.setPreferences(settingsPreferences);

        languageChoice.value = settingsPreferences.languageIndex;
        languageChoice.RefreshShownValue();

        textSliderMusic.text = (Math.Round(settingsPreferences.musicVolume*100))+"%";
        soundManager.updateMusciVolume(settingsPreferences.musicVolume);
        sliderMusic.value = settingsPreferences.musicVolume;

        textSliderSound.text = (Math.Round(settingsPreferences.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(settingsPreferences.soundEffectsVolume);
        sliderSound.value = settingsPreferences.soundVolume;

        if(settingsPreferences.showPrompts)
            toggleShowPrompts.isOn = true;
        else
            toggleShowPrompts.isOn = false;

        isInitialized = true;
    }


    public void SwitchToKeyBindings()
    {
        keyBindingScreen.SetActive(true);
        otherSettingsScreen.SetActive(false);
    }


    public void SwitchToOtherSettings()
    {
        keyBindingScreen.SetActive(false);
        otherSettingsScreen.SetActive(true);
    }


    public void ChangeLanguage(int languageIndex)
    {
        if (!isInitialized)
        {
            return;
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        settingsPreferences.languageIndex = languageIndex;
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Sound goes here
    }


    public void onMusicVolumeChange()
    {
        if (!isInitialized)
        {
            return;
        }

        settingsPreferences.musicVolume = sliderMusic;
        textSliderMusic.text = (Math.Round(settingsPreferences.musicVolume*100))+"%";
        soundManager.updateMusciVolume(settingsPreferences.musicVolume);
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Sound goes here
    }

    public void onSoundVolumeChange()
    {
        if (!isInitialized)
        {
            return;
        }

        settingsPreferences.soundVolume = sliderSound;
        textSliderSound.text = (Math.Round(settingsPreferences.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(settingsPreferences.soundEffectsVolume);
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Sound goes here
    }


    public void onCheckPromtsForKeys()
    {
        if (!isInitialized)
        {
            return;
        }

        if(settingsPreferences.showPrompts)
            settingsPreferences.showPrompts = false;
        else
            settingsPreferences.showPrompts = true;
        
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Sound goes here
    }
}