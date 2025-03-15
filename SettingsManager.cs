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


    private SettingsPreferences settingsPreferences;


    private void Start()
    {
        SwitchToOtherSettings();

        settingsPreferences = SaveSystem.LoadSettingsPreferences();
        if(settingsPreferences==null)
            settingsPreferences = createPreferences();

        GameInfo.setPreferences(settingsPreferences);

        languageChoice.value = GameInfo.languageIndex;
        languageChoice.RefreshShownValue();
        textSliderMusic.text = (Math.Round(GameInfo.musicVolume*100))+"%";
        soundManager.updateMusciVolume(GameInfo.musicVolume);
        textSliderSound.text = (Math.Round(GameInfo.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(GameInfo.soundEffectsVolume);
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
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        GameInfo.languageIndex = languageIndex;
    }


    public void onMusicVolumeChange()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        GameInfo.musicVolume = sliderMusic.value;
        textSliderMusic.text = (Math.Round(GameInfo.musicVolume*100))+"%";
        soundManager.updateMusciVolume(GameInfo.musicVolume);

        //settingsChanged=true;
        //soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void onSoundVolumeChange()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        GameInfo.soundEffectsVolume = sliderSound.value;
        textSliderSound.text = (Math.Round(GameInfo.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(GameInfo.soundEffectsVolume);

        //settingsChanged=true;
        //soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }
}