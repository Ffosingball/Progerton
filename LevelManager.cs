using UnityEngine;
using System.Collections.Generic;

/*This class manages all events and all non-movement
non-rotation inputs*/

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 20f; //Max time for one round
    [SerializeField]
    private float countdownTime = 3f;
    [SerializeField]
    private Vector3[] initialPositionsCharacter; //for the character
    [SerializeField]
    private Quaternion[] initialRotationsCharacter; //for the character
    [SerializeField]
    private Vector3 initialPositionCamera = new Vector3(0,0,0); //for the camera
    [SerializeField]
    private Quaternion initialRotationCamera = Quaternion.Euler(0,0,0); //for the camera
    [SerializeField]
    private KeyCode changeModeKey = KeyCode.Q;
    [SerializeField]
    private KeyCode replay_rerecordKey = KeyCode.R;
    [SerializeField]
    private KeyCode endRecordingKey = KeyCode.E;

    
    public GameObject character;
    public GameObject camera;
    public UIManager uIManager;
    public SavePlayerMovements savePlayerMovements;
    public ReplayManager replayManager; 
    public TriggersManager triggersManager;

    //false - camera mode, true - recording mode
    private bool gameMode;
    private bool canMove;
    private float currentTime = 0;
    private Coroutine recording=null;


    public Vector3[] getInitialPositions(){return initialPositionsCharacter;}
    public Quaternion[] getInitialRotations(){return initialRotationsCharacter;}
    public bool getCanMove(){return canMove;}


    public void Start()
    {
        camera.transform.position = initialPositionCamera;
        camera.transform.rotation = initialRotationCamera;
    }


    public void resetPosition()
    {
        character.transform.position = initialPositionsCharacter[replayManager.getCurrentRound()];
        character.transform.rotation = initialRotationsCharacter[replayManager.getCurrentRound()];
    }


    public void Update()
    {
        if(Input.GetKeyDown(changeModeKey))
        {
            ChangeMode();
        }

        if(Input.GetKeyDown(replay_rerecordKey))
        {
            if(gameMode==false)
                replayManager.StartReplay();
            else
                uIManager.rerecordWarning();
        }

        if(Input.GetKeyDown(endRecordingKey) && gameMode)
        {
            uIManager.endRecordingWarning();
        }
    }


    public void changeToOverviewMode()
    {
        savePlayerMovements.StopRecording();
        replayManager.StopReplay();
        setOverviewMode();
    }


    public void setOverviewMode()
    {
        character.SetActive(false);
        camera.SetActive(true);
        gameMode = false;
        canMove=false;
        uIManager.setLevelOverviewScreen();
    }


    public void rerecordMoves()
    {
        savePlayerMovements.StopRecording();
        replayManager.StopReplay();
        resetPosition();

        StopCoroutine(recording);
        recording = StartCoroutine(countdown());
    }


    //changes mode from recording to the camera
    public void ChangeMode()
    {
        if(gameMode)
        {
            uIManager.changeModeWarning();
        }
        else
        {
            camera.SetActive(false);
            character.SetActive(true);
            gameMode = true;
            uIManager.setCountdownScreen();
            recording = StartCoroutine(countdown());
        }
    }


    private IEnumerator<WaitForSeconds> countdown()
    {
        currentTime=countdownTime;
        while (currentTime>0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            uIManager.outputCountdown(currentTime);
        }

        uIManager.setGameScreen();
        savePlayerMovements.StartRecording();
        replayManager.StartReplay();

        recording = StartCoroutine(timer());
    }


    private IEnumerator<WaitForSeconds> timer()
    {
        canMove=true;
        currentTime=0;
        while (currentTime<maxTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime+=0.1f;
            uIManager.outputTimer(currentTime);

            if(triggersManager.getEndGame())
                uIManager.setWinScreen();
        }

        if(replayManager.isLastRound())
        {
            uIManager.setLostScreen();
        }
        else
        {
            uIManager.setEndRoundScreen();
        }
    }


    public void goToNextRound()
    {
        savePlayerMovements.StopRecording();
        replayManager.NextRound();
    }


    public void goToPreviousRound()
    {
        savePlayerMovements.StopRecording();
        replayManager.PreviousRound();
    }
}