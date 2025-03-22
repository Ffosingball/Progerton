using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;


/*This class manages all ui changes*/

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject escapeScreen;
    [SerializeField]
    private GameObject levelOverviewScreen;
    [SerializeField]
    private GameObject countdownScreen;
    [SerializeField]
    private GameObject changeModeScreen;
    [SerializeField]
    private GameObject rerecordScreen;
    [SerializeField]
    private GameObject endRecordingScreen;
    [SerializeField]
    private GameObject endRoundScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject lostScreen;
    [SerializeField]
    private GameObject prevRoundScreen;
    [SerializeField]
    private GameObject settingsScreen;
    //[SerializeField]
    //private TMP_Text isRecordingText;
    [SerializeField]
    private TMP_Text lastFirstRoundText;
    [SerializeField]
    private TMP_Text roundText;
    [SerializeField]
    private TMP_Text errorText;
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private TMP_Text timerText, timeText2;
    [SerializeField]
    private TMP_Text levelText1, levelText2, levelText3;
    [SerializeField]
    private GameObject overviewPrompts, gamePrompts;
    [SerializeField]
    private Button prevRoundBut;
    [SerializeField]
    private Button nextLevelBut;
    [SerializeField]
    private float timeBeforeTextDisappear=5f;

    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;

    public SettingsManager settingsManager;
    public LevelManager levelManager;
    public ReplayManager replayManager;

    private bool cursorlocked;
    private Coroutine disappearText=null;
    private LevelData data;
    private GameObject curScreen;
    private int roundNum=-1;


    //Getter for cursorLocked
    public bool getCursorlocked(){return cursorlocked;}

    
    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
        lastFirstRoundText.text = "";
        Time.timeScale = 1f;

        data = SaveSystem.LoadLevelData();

        if(data.sceneName.Count==GameInfo.currentLevel-1)
            nextLevelBut.interactable = false;
        else
            nextLevelBut.interactable = true;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && cursorlocked)//open pause menu
        {
            cursorlocked=false;
            Cursor.lockState = CursorLockMode.None;
            escapeScreen.SetActive(true);
            Time.timeScale = 0f;
            //Debug.Log("Open");
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) //return to game
        {
            ReturnToGame();
        }

        //Check status and return
        /*if(savePlayerMovements.getIsRecording())
            isRecordingText.text = "Recording...";
        else
            isRecordingText.text = "...";*/

        if(replayManager.getCurrentRound()==0)
            prevRoundBut.interactable = false;
        else
            prevRoundBut.interactable = true;
        
        if(settingsManager.getSettingsPreferences().showPrompts)
        {
            overviewPrompts.SetActive(true);
            gamePrompts.SetActive(true);
        }
        else
        {
            overviewPrompts.SetActive(false);
            gamePrompts.SetActive(false);
        }


        if(settingsManager.getLanguageChanged())
        {
            updateText();
        }
    }


    private void updateText()
    {
        _currentStringTable = _localizedStringTable.GetTable();

        roundText.text = _currentStringTable["roundT"].LocalizedValue+" "+roundNum;

        levelText1.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1)+" "+_currentStringTable["completed"].LocalizedValue;
        levelText2.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1)+" "+_currentStringTable["failure"].LocalizedValue;
        levelText3.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1); 

        TMP_Text t1 = gamePrompts.GetComponent<TMP_Text>();
        t1.text = "W, S, D, A - "+_currentStringTable["move"].LocalizedValue.Replace("\n", "")+"\nShift - "+_currentStringTable["run"].LocalizedValue.Replace("\n", "")+"\nEscape - "+_currentStringTable["exit"].LocalizedValue.Replace("\n", "")+"\nE - "+_currentStringTable["end_recording"].LocalizedValue.Replace("\n", "")+"\nR - "+_currentStringTable["rerecord_moves"].LocalizedValue.Replace("\n", "")+"\nQ - "+_currentStringTable["overview"].LocalizedValue.Replace("\n", "");

        TMP_Text t2 = overviewPrompts.GetComponent<TMP_Text>();
        t2.text = "W, S, D, A - "+_currentStringTable["move"].LocalizedValue.Replace("\n", "")+"\nShift, Space - "+_currentStringTable["move_up_and_down"].LocalizedValue.Replace("\n", "")+"\nEscape - "+_currentStringTable["exit"].LocalizedValue.Replace("\n", "")+"\nR - "+_currentStringTable["replay_movements"].LocalizedValue.Replace("\n", "")+"\nQ - "+_currentStringTable["start_recording"].LocalizedValue.Replace("\n", "")+"\nE - "+_currentStringTable["prev_round"].LocalizedValue.Replace("\n", "");
    }


    //Unpause the game
    public void ReturnToGame()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        escapeScreen.SetActive(false);
        settingsScreen.SetActive(false);
        Time.timeScale = 1f;
    }


    public void OutputRoundStatus(int output)
    {
        if(disappearText!=null)
        {
            StopCoroutine(disappearText);
            disappearText=null;
        }
	
	switch(output)
	{
	    case 1:
        	lastFirstRoundText.text = _currentStringTable["first_round"].LocalizedValue;
		break;
	    case 2:
        	lastFirstRoundText.text = _currentStringTable["last_round"].LocalizedValue;
		break;
	}
        disappearText = StartCoroutine(hideTextTimer());
    }


    private IEnumerator<WaitForSeconds> hideTextTimer()
    {
        yield return new WaitForSeconds(timeBeforeTextDisappear);
        lastFirstRoundText.text = "";
        disappearText = null;
    }


    public void OutputRoundNumber(int num)
    {
        roundNum=num;
    }


    public void OutputErrorText()
    {
        errorText.text = _currentStringTable["error_message"].LocalizedValue;
    }


    public void outputCountdown(float countdownTime)
    {
        countdownText.text = countdownTime+"";
    }


    public void outputTimer(float time)
    {
        timerText.text = time+"";
    }


    public void setLevelOverviewScreen()
    {
        gameScreen.SetActive(false);
        levelOverviewScreen.SetActive(true);
        curScreen = levelOverviewScreen;
    }


    public void setCountdownScreen()
    {
        levelOverviewScreen.SetActive(false);
        countdownScreen.SetActive(true);
        curScreen = countdownScreen;
    }


    public void setGameScreen()
    {
        countdownScreen.SetActive(false);
        gameScreen.SetActive(true);
        curScreen = gameScreen;
    }


    public void changeModeWarning()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        changeModeScreen.SetActive(true);
    }


    public void noChangeMode()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
    }


    public void yesChangeMode()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
        levelManager.changeToOverviewMode();
    }


    public void rerecordWarning()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        rerecordScreen.SetActive(true);
    }


    public void noRerecord()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
    }


    public void yesRerecord()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
        levelManager.rerecordMoves();
    }


    public void endRecordingWarning()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        endRecordingScreen.SetActive(true);
    }


    public void noEndRecord()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        endRecordingScreen.SetActive(false);
    }


    public void yesEndRecord()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        endRecordingScreen.SetActive(false);
        setEndRoundScreen();
    }


    public void setEndRoundScreen()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        endRoundScreen.SetActive(true);
    }


    public void setWinScreen()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        winScreen.SetActive(true);

        timeText2.text = _currentStringTable["time"].LocalizedValue+levelManager.getTime();

        if(data.sceneName.Count!=GameInfo.currentLevel-1)
            data.locked[GameInfo.currentLevel+1] = false;
        
        if(data.bestTime[GameInfo.currentLevel]>levelManager.getTime())
            data.bestTime[GameInfo.currentLevel] = levelManager.getTime();

        SaveSystem.SaveLevelData(data);
    }


    public void setLostScreen()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        lostScreen.SetActive(true);
    }


    public void startNextRound()
    {
        endRoundScreen.SetActive(false);
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        levelManager.goToNextRound();
    }


    public void startPreviousRound()
    {
        endRoundScreen.SetActive(false);
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        levelManager.goToPreviousRound();
    }


    public void exitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void nextLevel()
    {
        GameInfo.currentLevel++;
        SceneManager.LoadScene("level"+(GameInfo.currentLevel));
    }


    public void startLevelAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Rerecord()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        endRoundScreen.SetActive(false);
        levelManager.rerecordMoves();
    }


    public void prevRoundWarning()
    {
        cursorlocked=false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        prevRoundScreen.SetActive(true);
    }


    public void noPrevRound()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        prevRoundScreen.SetActive(false);
    }


    public void yesPrevRound()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        prevRoundScreen.SetActive(false);
        startPreviousRound();
    }


    public void OpenSettingsScreen()
    {
        escapeScreen.SetActive(false);
        settingsScreen.SetActive(true);
        curScreen.SetActive(false);
    }


    public void OpenEscapeScreen()
    {
        escapeScreen.SetActive(true);
        settingsScreen.SetActive(false);
        curScreen.SetActive(true);
    }
}
