using UnityEngine;
using System.Collections.Generic;
using System;

/*This class manages all events and all non-movement
non-rotation inputs*/

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 20f; //Max time for one round
    [SerializeField]
    private float countdownTime = 3f; //Countdown before start
    [SerializeField]
    private Vector3[] initialPositionsCharacter; //Initial position of the character in each round
    [SerializeField]
    private Vector3[] initialRotationsCharacter; //Initial rotation of the character in each round
    [SerializeField]
    private Vector3 initialPositionCamera = new Vector3(0,0,0); //initial position for the camera
    [SerializeField]
    private Vector3 initialRotationCamera = new Vector3(0,0,0); //initial rotation for the camera
    [SerializeField]
    private KeyCode changeModeKey = KeyCode.Q;
    [SerializeField]
    private KeyCode replay_rerecordKey = KeyCode.R;
    [SerializeField]
    private KeyCode endRecordingKey = KeyCode.E;
    [SerializeField]
    private KeyCode goToPrevRoundKey = KeyCode.E;

    
    public GameObject character;
    public GameObject cameraOfTheCharacter;
    public GameObject camera;
    public UIManager uIManager;
    public SavePlayerMovements savePlayerMovements;
    public ReplayManager replayManager; 
    public TriggersManager triggersManager;
    public PersonLook personLook;

    private bool gameMode; //false - camera mode, true - recording mode
    private bool canMove; //it is used to disable movement of the character during countdown
    private float currentTime = 0;
    private Coroutine recording=null;


    //getters
    public Vector3[] getInitialPositions(){return initialPositionsCharacter;}
    public Vector3[] getInitialRotations(){return initialRotationsCharacter;}
    public bool getCanMove(){return canMove;}
    //public bool getMoveCamera(){return moveCamera;}


    //Set initial positions
    public void Start()
    {
        camera.transform.position = initialPositionCamera;
        camera.transform.rotation = Quaternion.Euler(initialRotationCamera);
        canMove=false;
    }


    //Resets position of the character depending on the current round
    public void resetPosition()
    {
        int round = replayManager.getCurrentRound();
        //moveCamera = false;
        character.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,0);
        character.transform.position = initialPositionsCharacter[round];
        character.transform.localRotation = Quaternion.Euler(initialRotationsCharacter[round]);
    
        Vector3 newRotation = initialRotationsCharacter[round];
        // Convert rotation to match script's `velocity`
        personLook.setVelocity(new Vector2(0, -newRotation.x)); 
    }


    //Check if any of the keys has been pressed
    public void Update()
    {
        /*if(!moveCamera)
            moveCamera=true;*/

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
            if(replayManager.isLastRound())
                uIManager.OutputErrorText("You cannot do that on the last round!");
            else
                uIManager.endRecordingWarning();
        }

        if(Input.GetKeyDown(goToPrevRoundKey) && !gameMode)
        {
            if(replayManager.getCurrentRound()==0)
                uIManager.OutputRoundStatus("This is the first Round!");
            else
                uIManager.prevRoundWarning();
        }
    }


    //Change mode to overview mode
    public void changeToOverviewMode()
    {
        savePlayerMovements.StopRecording();
        replayManager.StopReplay();
        setOverviewMode();
    }


    public void setOverviewMode()
    {
        if(recording!=null)
        {
            StopCoroutine(recording);
            recording=null;
        }

        character.SetActive(false);
        camera.SetActive(true);
        //camera.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,0);
        gameMode = false;
        canMove=false;
        uIManager.setLevelOverviewScreen();
    }


    public void rerecordMoves()
    {
        if(recording!=null)
        {
            StopCoroutine(recording);
            recording=null;
        }

        savePlayerMovements.StopRecording();
        replayManager.StopReplay();
        canMove=false;
        uIManager.setLevelOverviewScreen();

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
            recording = StartCoroutine(countdown());
        }
    }


    private IEnumerator<WaitForSeconds> countdown()
    {
        resetPosition();
        uIManager.setCountdownScreen();
        replayManager.StopReplay();

        currentTime=countdownTime;
        uIManager.outputCountdown(currentTime);
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
            yield return new WaitForSeconds(0.02f);
            currentTime+=0.02f;
            uIManager.outputTimer((float)Math.Round((double)currentTime,2));

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
        if(recording!=null)
        {
            StopCoroutine(recording);
            recording=null;
        }
        savePlayerMovements.StopRecording();
        replayManager.NextRound();
    }


    public void goToPreviousRound()
    {
        if(recording!=null)
        {
            StopCoroutine(recording);
            recording=null;
        }
        savePlayerMovements.StopRecording();
        replayManager.PreviousRound();
    }
}