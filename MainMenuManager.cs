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
    private GameObject statisticsScreen;
    [SerializeField]
    private TMP_Text madeBy;
    [SerializeField]
    private TMP_Text bridgesValue, gatesValue, platformsValue, replaysValue, shortestValue, shortestLevel, longestValue, longestLevel, walkedValue, flewValue, timeSpentValue;
    [SerializeField]
    private string username;
    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;

    public SettingsManager settingsManager;
    public GameTimeCounter gameTimeCounter;


    private void Start()
    {
        OpenMainMenuScreen();
        updateText();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        if(GameInfo.gameStatistics==null)
            GameInfo.getStatistics();
    }


    private void Update()
    {
        if(settingsManager.getLanguageChanged())
        {
            updateText();
        }

        timeSpentValue.text = gameTimeCounter.GetFormattedTime();
    }


    private void updateText()
    {
        _currentStringTable = _localizedStringTable.GetTable();

        madeBy.text = _currentStringTable["madeBy"].LocalizedValue+" "+username;
    }


    public void ExitGame()
    {
        GameInfo.SaveData();
        Debug.Log("Saved");
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
        statisticsScreen.SetActive(false);
    }

    public string getTextForKeyRebinding()
    {
        return _currentStringTable["press_key"].LocalizedValue;
    }


    public void OpenStatisticsScreen()
    {
        mainMenuScreen.SetActive(false);

        bridgesValue.text = GameInfo.gameStatistics.bridgesActivated.ToString();
        gatesValue.text = GameInfo.gameStatistics.gatesOpened.ToString();
        platformsValue.text = GameInfo.gameStatistics.platformsActivated.ToString();
        replaysValue.text = GameInfo.gameStatistics.numOfReplaysMade.ToString();

        if(GameInfo.gameStatistics.shortestTime==99999)
        {
            shortestValue.text = _currentStringTable["none"].LocalizedValue;
            shortestLevel.text = _currentStringTable["none"].LocalizedValue;
        }
        else
        {
            shortestValue.text = GameInfo.gameStatistics.shortestTime.ToString();
            shortestLevel.text = (GameInfo.gameStatistics.shortestTimeAtLevel+1).ToString();
        }

        if(GameInfo.gameStatistics.longestTime==0)
        {
            longestValue.text = _currentStringTable["none"].LocalizedValue;
            longestLevel.text = _currentStringTable["none"].LocalizedValue;
        }
        else
        {
            longestValue.text = GameInfo.gameStatistics.longestTime.ToString();
            longestLevel.text = (GameInfo.gameStatistics.longestTimeAtLevel+1).ToString();
        }

        walkedValue.text = GameInfo.convertDistance(GameInfo.gameStatistics.distanceWalked);
        flewValue.text = GameInfo.convertDistance(GameInfo.gameStatistics.distanceFlew);

        statisticsScreen.SetActive(true);
    }
}