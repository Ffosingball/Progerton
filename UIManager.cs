using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private TMP_Text isRecordingText;
    [SerializeField]
    private TMP_Text lastFirstRoundText;
    [SerializeField]
    private TMP_Text roundText;
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private TMP_Text timerText;

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
        Time.timeScale = 0f;
        changeModeScreen.SetActive(true);
    }


    public void noChangeMode()
    {
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
    }


    public void yesChangeMode()
    {
        Time.timeScale = 1f;
        changeModeScreen.SetActive(false);
        levelManager.setOverviewMode();
    }


    public void rerecordWarning()
    {
        Time.timeScale = 0f;
        rerecordScreen.SetActive(true);
    }


    public void noRerecord()
    {
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
    }


    public void yesRerecord()
    {
        Time.timeScale = 1f;
        rerecordScreen.SetActive(false);
        levelManager.rerecordMoves();
    }
}
