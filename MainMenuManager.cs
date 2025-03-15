using UnityEngine;
using UnityEngine.UI;
using TMPro;
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


    private void Start()
    {
        OpenMainMenuScreen();
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
}