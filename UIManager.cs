using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private TMP_Text isRecordingText;
    [SerializeField]
    private TMP_Text lastFirstRoundText;
    [SerializeField]
    private TMP_Text roundText;
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private TMP_Text timerText, timeText2;

    public SavePlayerMovements savePlayerMovements;
    public LevelManager levelManager;

    private bool cursorlocked;


    //Getter for cursorLocked
    public bool getCursorlocked(){return cursorlocked;}

    
    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
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
        if(savePlayerMovements.getIsRecording())
            isRecordingText.text = "Recording...";
        else
            isRecordingText.text = "...";
    }


    //Unpause the game
    public void ReturnToGame()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        escapeScreen.SetActive(false);
        Time.timeScale = 1f;
    }


    public void OutputRoundStatus(string output)
    {
        lastFirstRoundText.text = output;
    }


    public void OutputRoundNumber(string rText)
    {
        roundText.text = rText;
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
    }


    public void setCountdownScreen()
    {
        levelOverviewScreen.SetActive(false);
        countdownScreen.SetActive(true);
    }


    public void setGameScreen()
    {
        countdownScreen.SetActive(false);
        gameScreen.SetActive(true);
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
        timeText2.text = "Time go here!";
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
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        levelManager.goToNextRound();
    }


    public void startPreviousRound()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        levelManager.goToPreviousRound();
    }


    public void exitLevel()
    {
        Debug.Log("Level exited successfully!");
    }


    public void nextLevel()
    {
        Debug.Log("Next level loaded successfuly!");
    }


    public void startLevelAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
