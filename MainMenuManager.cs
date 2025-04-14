using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

/*This class manages all ui changes in main menu*/

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private GameObject levelsScreen;
    [SerializeField]
    private GameObject settingsScreen;
    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;

    public SettingsManager settingsManager;


    private void Start()
    {
        OpenMainMenuScreen();
        updateText();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }


    private void Update()
    {
        if(settingsManager.getLanguageChanged())
        {
            updateText();
        }
    }


    private void updateText()
    {
        _currentStringTable = _localizedStringTable.GetTable();
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

    public string getTextForKeyRebinding()
    {
        return _currentStringTable["press_key"].LocalizedValue;
    }
}