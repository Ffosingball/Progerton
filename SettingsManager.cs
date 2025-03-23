using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System;

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
    private Slider sliderMusic, sliderSound, sliderSensitivity;
    [SerializeField]
    private TMP_Text textSliderMusic, textSliderSound;
    [SerializeField]
    private TMP_Dropdown languageChoice;
    [SerializeField]
    private Toggle toggleShowPrompts, toggleWarningScreen;
    [SerializeField]
    private TMP_FontAsset japaneseFont;
    [SerializeField]
    private TMP_FontAsset otherLanguageFont;


    private SettingsPreferences settingsPreferences;
    private bool isInitialized = false;
    private bool languageChanged=false;
    private Coroutine changeLanguage;


    public SoundManager soundManager;
    public KeyRebinder keyRebinder;


    public SettingsPreferences getSettingsPreferences(){return settingsPreferences;}
    public bool getLanguageChanged(){return languageChanged;}


    private void Start()
    {
        SwitchToOtherSettings();

        settingsPreferences = SaveSystem.LoadSettingsPreferences();
        if(settingsPreferences==null)
            settingsPreferences = createPreferences();

        languageChoice.value = settingsPreferences.languageIndex;
        languageChoice.RefreshShownValue();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[settingsPreferences.languageIndex];

        textSliderMusic.text = (Math.Round(settingsPreferences.musicVolume*100))+"%";
        soundManager.updateMusciVolume(settingsPreferences.musicVolume);
        sliderMusic.value = settingsPreferences.musicVolume;

        textSliderSound.text = (Math.Round(settingsPreferences.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(settingsPreferences.soundEffectsVolume);
        sliderSound.value = settingsPreferences.soundEffectsVolume;

        sliderSensitivity.value = settingsPreferences.sensitivity;

        if(settingsPreferences.showPrompts)
            toggleShowPrompts.isOn = true;
        else
            toggleShowPrompts.isOn = false;

        if(settingsPreferences.showWarningsScreen)
            toggleWarningScreen.isOn = true;
        else
            toggleWarningScreen.isOn = false;

        isInitialized = true;

        UpdateFont();

        languageChanged=true;

        keyRebinder.updateText();

        changeLanguage = StartCoroutine(waitForChanges());
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
        UpdateFont();

        languageChanged=true;

        if(changeLanguage!=null)
        {
            StopCoroutine(changeLanguage);
            changeLanguage = null;
        }

        changeLanguage = StartCoroutine(waitForChanges());

        //Sound goes here
    }


    private void UpdateFont()
    {
        string currentLanguage = LocalizationSettings.SelectedLocale.Identifier.Code;
        TMP_FontAsset selectedFont = (currentLanguage == "ja") ? japaneseFont : otherLanguageFont;

        foreach (TextMeshProUGUI text in FindObjectsOfType<TextMeshProUGUI>(true)) // true includes inactive objects
        {
            text.font = selectedFont;
        }
    }


    private IEnumerator<WaitForSeconds> waitForChanges()
    {
        yield return new WaitForSeconds(0.2f);
        languageChanged = false;
        changeLanguage = null;
    }


    public void onMusicVolumeChange()
    {
        if (!isInitialized)
        {
            return;
        }

        settingsPreferences.musicVolume = sliderMusic.value;
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

        settingsPreferences.soundEffectsVolume = sliderSound.value;
        textSliderSound.text = (Math.Round(settingsPreferences.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(settingsPreferences.soundEffectsVolume);
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Sound goes here
    }


    public void onSensitivityChange()
    {
        if (!isInitialized)
        {
            return;
        }

        settingsPreferences.sensitivity = sliderSensitivity.value;
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

        //Debug.Log("Called");

        //Sound goes here
    }


    public void onCheckWarningsScreen()
    {
        if (!isInitialized)
        {
            return;
        }

        if(settingsPreferences.showWarningsScreen)
            settingsPreferences.showWarningsScreen = false;
        else
            settingsPreferences.showWarningsScreen = true;
        
        SaveSystem.SaveSettingsPreferences(settingsPreferences);

        //Debug.Log("Called");

        //Sound goes here
    }


    private SettingsPreferences createPreferences()
    {
        SettingsPreferences pref = new SettingsPreferences();
        pref.languageIndex = 0;
        pref.soundEffectsVolume = 1f;
        pref.musicVolume = 1f;
        pref.showPrompts = true;
        pref.showWarningsScreen = true;
        pref.sensitivity = 1f;

        pref.keyBindings = new string[13];
        pref.keyBindings[0] = "<Keyboard>/w";
        pref.keyBindings[1] = "<Keyboard>/s";
        pref.keyBindings[2] = "<Keyboard>/a";
        pref.keyBindings[3] = "<Keyboard>/d";
        pref.keyBindings[4] = "<Keyboard>/space";
        pref.keyBindings[5] = "<Keyboard>/shift";
        pref.keyBindings[6] = "<Keyboard>/shift";
        pref.keyBindings[7] = "<Keyboard>/q";
        pref.keyBindings[8] = "<Keyboard>/r";
        pref.keyBindings[9] = "<Keyboard>/e";
        pref.keyBindings[10] = "<Keyboard>/e";
        pref.keyBindings[11] = "<Keyboard>/r";
        pref.keyBindings[12] = "<Keyboard>/q";

        return pref;
    }
}