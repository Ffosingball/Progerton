using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using System.Collections;

/*This class manages all ui changes in main menu*/

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private GameObject levelsScreen;
    [SerializeField]
    private GameObject settingsScreen;

    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;


    private void Start()
    {
        OpenMainMenuScreen();

        LoadLocalizedText();
    }


    public void ExitGame()
    {
        Application.Quit();
    }


    public void OpenSettingsScreen()
    {
        mainMenuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }


    public void OpenLevelsScreen()
    {
        mainMenuScreen.SetActive(false);
        levelsScreen.SetActive(true);
    }


    public void OpenMainMenuScreen()
    {
        mainMenuScreen.SetActive(true);
        levelsScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }


    public void LoadLocalizedText()
    {
        // 2. Wait for the table to load asynchronously
        _currentStringTable = _localizedStringTable.GetTable();
        // At this point _currentStringTable can be used to
        // access our strings
        // 3. Retrieve the localized string
        string str = _currentStringTable["check_text"].LocalizedValue;
        Debug.Log(str);
    }
}