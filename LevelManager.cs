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

    
    public GameObject character;
    public GameObject camera;
    public UIManager uIManager;
    public SavePlayerMovements savePlayerMovements;
    public ReplayManager replayManager; 
    public TriggersManager triggersManager;
    public PersonLook personLook;
    public SettingsManager settingsManager;

    private bool gameMode; //false - camera mode, true - recording mode
    private bool canMove; //it is used to disable movement of the character during countdown
    private float currentTime = 0;
    private Coroutine recording=null;
    private bool stopCounting=false;


    //getters
    public Vector3[] getInitialPositions(){return initialPositionsCharacter;}
    public Vector3[] getInitialRotations(){return initialRotationsCharacter;}
    public bool getCanMove(){return canMove;}
    public void setCanMove(bool canMove){this.canMove = canMove; }
    //public bool getMoveCamera(){return moveCamera;}
    public float getTime() {return (float)Math.Round((double)(maxTime-currentTime),2);}
    public void setStopCounting(bool stopCounting){this.stopCounting=stopCounting;}


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
    
        // Convert rotation to match script's `velocity`
        personLook.setVelocity(new Vector2(initialRotationsCharacter[round].y, 0));
        //Debug.Log("L: "+character.transform.localRotation);
    }


    public void handleEndRecording()
    {
        if(replayManager.isLastRound())
            uIManager.OutputErrorText();
        else
        {
            if(settingsManager.getSettingsPreferences().showWarningsScreen)
                uIManager.endRecordingWarning();
            else
                uIManager.yesEndRecord();
        }
    }


    public void handlePreviousRound()
    {
        if(replayManager.getCurrentRound()==0)
            uIManager.OutputRoundStatus(1);
        else
        {
            if(settingsManager.getSettingsPreferences().showWarningsScreen)
                uIManager.prevRoundWarning();
            else
                uIManager.yesPrevRound();
        }
    }


    public void handleRerecordMoves()
    {
        if(settingsManager.getSettingsPreferences().showWarningsScreen)
            uIManager.rerecordWarning();
        else
            uIManager.yesRerecord();
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
        character.GetComponent<Movement>().setMoveInput(new Vector2(0,0));
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
            if(settingsManager.getSettingsPreferences().showWarningsScreen)
                uIManager.changeModeWarning();
            else
                uIManager.yesChangeMode();
        }
        else
        {
            camera.SetActive(false);
            character.SetActive(true);
            gameMode = true;
            camera.GetComponent<CameraMovement>().setMoveInput(new Vector2(0,0));
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
        currentTime=maxTime;
        while (currentTime>0)
        {
            yield return new WaitForSeconds(0.02f);
            if(!stopCounting)
            {
                currentTime-=0.02f;
                uIManager.outputTimer((float)Math.Round((double)currentTime,2));

                if(triggersManager.getEndGame())
                    uIManager.setWinScreen();
            }
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