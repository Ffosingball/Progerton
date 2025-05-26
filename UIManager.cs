using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using System;
using UnityEngine.Audio;


/*This class manages all ui changes*/

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
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
    private TMP_Text[] roundText;
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
    private AudioClip countdown;

    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;

    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Image loadingFillBar;

    public SettingsManager settingsManager;
    public LevelManager levelManager;
    public ReplayManager replayManager;
    public KeyRebinder keyRebinder;
    public AudioMixer mixer;
    public SoundManager soundManager;
    public AudioSource countdownAudioSource;
    public Movement playerMovement;

    private bool cursorlocked, musicIsMuffled, otherScreenOpened = false;
    private Coroutine disappearText=null, disappearText2=null;
    private LevelData data;
    private GameObject curScreen;
    private int roundNum=-1;


    //Getter for cursorLocked
    public bool getCursorlocked(){return cursorlocked;}

    
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
        lastFirstRoundText.text = "";
        errorText.text = "";
        Time.timeScale = 1f;
        mixer.SetFloat("muffleFrequency", 22000f);
        musicIsMuffled = false;
        

        data = SaveSystem.LoadLevelData();

        if(data.sceneName.Count==GameInfo.currentLevel-1)
            nextLevelBut.interactable = false;
        else
            nextLevelBut.interactable = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cursorlocked)//open pause menu
        {
            soundManager.PauseSound();
            soundManager.playKeyPressedSound();
            cursorlocked = false;
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            Cursor.lockState = CursorLockMode.None;
            escapeScreen.SetActive(true);
            Time.timeScale = 0f;
            MuffleMusic();
            countdownAudioSource.Pause();
            playerMovement.dealWithFilter();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //return to game
        {
            if (!otherScreenOpened)
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
            //Debug.Log("Show");
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

        foreach(TMP_Text text in roundText)
        {
            text.text = _currentStringTable["roundT"].LocalizedValue+" "+roundNum;
        }
    }


    public void updateText()
    {
        //Debug.Log("Updated text!");
        _currentStringTable = _localizedStringTable.GetTable();

        levelText1.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1)+" "+_currentStringTable["completed"].LocalizedValue;
        levelText2.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1)+" "+_currentStringTable["failure"].LocalizedValue;
        levelText3.text = _currentStringTable["level"].LocalizedValue +" "+ (GameInfo.currentLevel+1); 

        TMP_Text t1 = gamePrompts.GetComponent<TMP_Text>();
        string[] parts = new string[6];
        string compositeKey = "";

        compositeKey = keyRebinder.GetButtonTextMove(0);
        compositeKey = compositeKey + ", " + keyRebinder.GetButtonTextMove(1);
        compositeKey = compositeKey + ", " + keyRebinder.GetButtonTextMove(2);
        compositeKey = compositeKey + ", " + keyRebinder.GetButtonTextMove(3);
        parts[0] = compositeKey+" - "+_currentStringTable["move"].LocalizedValue.Replace("\n", "");
        parts[1] = "\n"+keyRebinder.GetButtonText(6)+" - "+_currentStringTable["run"].LocalizedValue.Replace("\n", "");
        parts[2] = "\nEscape - "+_currentStringTable["exit"].LocalizedValue.Replace("\n", "");
        parts[3] = "\n"+keyRebinder.GetButtonText(10)+" - "+_currentStringTable["end_recording"].LocalizedValue.Replace("\n", "");
        parts[4] = "\n"+keyRebinder.GetButtonText(11)+" - "+_currentStringTable["rerecord_moves"].LocalizedValue.Replace("\n", "");
        parts[5] = "\n"+keyRebinder.GetButtonText(12)+" - "+_currentStringTable["overview"].LocalizedValue.Replace("\n", "");

        t1.text = parts[0]+parts[1]+parts[2]+parts[3]+parts[4]+parts[5];

        TMP_Text t2 = overviewPrompts.GetComponent<TMP_Text>();

        parts[0] = compositeKey+" - "+_currentStringTable["move"].LocalizedValue.Replace("\n", "");
        compositeKey = keyRebinder.GetButtonText(4)+ ", " +keyRebinder.GetButtonText(5);
        parts[1] = "\n"+compositeKey+" - "+_currentStringTable["move_up_and_down"].LocalizedValue.Replace("\n", "");
        parts[2] = "\nEscape - "+_currentStringTable["exit"].LocalizedValue.Replace("\n", "");
        parts[3] = "\n"+keyRebinder.GetButtonText(8)+" - "+_currentStringTable["replay_movements"].LocalizedValue.Replace("\n", "");
        parts[4] = "\n"+keyRebinder.GetButtonText(7)+" - "+_currentStringTable["start_recording"].LocalizedValue.Replace("\n", "");
        parts[5] = "\n"+keyRebinder.GetButtonText(9)+" - "+_currentStringTable["prev_round"].LocalizedValue.Replace("\n", "");

        t2.text = parts[0]+parts[1]+parts[2]+parts[3]+parts[4]+parts[5];
    }


    //Unpause the game
    public void ReturnToGame()
    {
        soundManager.playKeyPressedSound();
        cursorlocked = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        escapeScreen.SetActive(false);
        settingsScreen.SetActive(false);
        curScreen.SetActive(true);
        MakeMusicClear();
        countdownAudioSource.UnPause();
        Time.timeScale = 1f;
        soundManager.ResumeSound();
        playerMovement.dealWithFilter();
    }


    public void OutputRoundStatus(int output)
    {
        if(disappearText!=null)
        {
            StopCoroutine(disappearText);
            disappearText=null;
        }

        switch (output)
        {
            case 1:
                lastFirstRoundText.text = _currentStringTable["first_round"].LocalizedValue;
                lastFirstRoundText.color = Color.red;
                break;
            case 2:
                lastFirstRoundText.text = _currentStringTable["last_round"].LocalizedValue;
                lastFirstRoundText.color = Color.red;
                break;
            case 3:
                lastFirstRoundText.text = _currentStringTable["prevRoundMessage"].LocalizedValue;
                lastFirstRoundText.color = new Color(1f, 1f, 1f);
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


    public string getTextForKeyRebinding()
    {
        return _currentStringTable["press_key"].LocalizedValue;
    }


    public void OutputRoundNumber(int num)
    {
        roundNum=num;
    }


    public void OutputErrorText()
    {
        if(disappearText2!=null)
        {
            StopCoroutine(disappearText2);
            disappearText2=null;
        }

        errorText.text = _currentStringTable["error_message"].LocalizedValue;
        disappearText2 = StartCoroutine(hideTextError());
    }



    private IEnumerator<WaitForSeconds> hideTextError()
    {
        yield return new WaitForSeconds(timeBeforeTextDisappear);
        errorText.text = "";
        disappearText2 = null;
    }


    public void outputCountdown(float countdownTime)
    {
        countdownText.text = countdownTime.ToString();
    }


    public void outputTimer(float time)
    {
        timerText.text = time.ToString("F2");
    }


    public void setLevelOverviewScreen()
    {
        gameScreen.SetActive(false);
        countdownScreen.SetActive(false);
        levelOverviewScreen.SetActive(true);
        curScreen = levelOverviewScreen;
        soundManager.ResumeMusic();
    }


    public void setCountdownScreen()
    {
        levelOverviewScreen.SetActive(false);
        countdownScreen.SetActive(true);
        curScreen = countdownScreen;
        soundManager.PauseMusic();
        countdownAudioSource.PlayOneShot(countdown);
    }


    public void setGameScreen()
    {
        countdownScreen.SetActive(false);
        gameScreen.SetActive(true);
        curScreen = gameScreen;
        soundManager.ResumeMusic();
    }


    public void changeModeWarning()
    {
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        MuffleMusic();
        Time.timeScale = 0f;
        changeModeScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void noChangeMode()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void yesChangeMode()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
        levelManager.changeToOverviewMode();
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void rerecordWarning()
    {
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        MuffleMusic();
        Time.timeScale = 0f;
        rerecordScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void noRerecord()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void yesRerecord()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
        levelManager.rerecordMoves();
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void endRecordingWarning()
    {
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        MuffleMusic();
        Time.timeScale = 0f;
        endRecordingScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void noEndRecord()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        endRecordingScreen.SetActive(false);
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void yesEndRecord()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        endRecordingScreen.SetActive(false);
        setEndRoundScreen();
    }


    public void setEndRoundScreen()
    {
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        soundManager.PauseMusic();
        Time.timeScale = 0f;
        endRoundScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void setWinScreen()
    {
        soundManager.playVictorySound();
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        soundManager.PauseMusic();
        Time.timeScale = 0f;
        winScreen.SetActive(true);
        soundManager.PauseSound();

        timeText2.text = _currentStringTable["time"].LocalizedValue+levelManager.getTime();

        if(data.sceneName.Count!=GameInfo.currentLevel-1)
            data.locked[GameInfo.currentLevel+1] = false;
        
        if(data.bestTime[GameInfo.currentLevel]>levelManager.getTime())
        {
            data.bestTime[GameInfo.currentLevel] = levelManager.getTime();
            GameInfo.setTime(levelManager.getTime());
        }

        GameInfo.SaveData();

        SaveSystem.SaveLevelData(data);

        otherScreenOpened = true;
    }


    public void setLostScreen()
    {
        soundManager.playLostSound();
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        soundManager.PauseMusic();
        Time.timeScale = 0f;
        lostScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void startNextRound()
    {
        endRoundScreen.SetActive(false);
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        soundManager.ResumeMusic();
        MakeMusicClear();
        Time.timeScale = 1f;
        levelManager.goToNextRound();
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void startPreviousRound()
    {
        endRoundScreen.SetActive(false);
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        soundManager.ResumeMusic();
        MakeMusicClear();
        Time.timeScale = 1f;
        levelManager.goToPreviousRound();
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void exitLevel()
    {
        GameInfo.SaveData();
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }


    public void nextLevel()
    {
        GameInfo.currentLevel++;
        GameInfo.SaveData();
        StartCoroutine(LoadSceneAsync("level"+(GameInfo.currentLevel)));
    }


    public void startLevelAgain()
    {
        GameInfo.SaveData();
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }


    public void Rerecord()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        soundManager.ResumeMusic();
        MakeMusicClear();
        Time.timeScale = 1f;
        endRoundScreen.SetActive(false);
        levelManager.rerecordMoves();
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void prevRoundWarning()
    {
        cursorlocked=false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.None;
        MuffleMusic();
        Time.timeScale = 0f;
        prevRoundScreen.SetActive(true);
        soundManager.PauseSound();
        otherScreenOpened = true;
    }


    public void noPrevRound()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
        MakeMusicClear();
        Time.timeScale = 1f;
        prevRoundScreen.SetActive(false);
        soundManager.ResumeSound();
        otherScreenOpened = false;
    }


    public void yesPrevRound()
    {
        cursorlocked=true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
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


    public void MuffleMusic()
    {
        mixer.SetFloat("muffleFrequency", 320f);
        float currentVolume;
        if(!musicIsMuffled)
        {
            mixer.GetFloat("MusicVolume", out currentVolume);
            mixer.SetFloat("MusicVolume", currentVolume-5);
            musicIsMuffled = true;
        }
    }


    public void MakeMusicClear()
    {
        mixer.SetFloat("muffleFrequency", 22000f);
        if(musicIsMuffled)
        {
            float currentVolume;
            mixer.GetFloat("MusicVolume", out currentVolume);
            mixer.SetFloat("MusicVolume", currentVolume+5);
            musicIsMuffled = false;
        }
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {

        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);

            loadingFillBar.fillAmount = progressValue;

            yield return null;
        }
    }
}
