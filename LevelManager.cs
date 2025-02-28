using UnityEngine;

/*This class manages all events and all non-movement
non-rotation inputs*/

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 20f; //Max time for one round
    [SerializeField]
    private float countdownTime = 3f;
    [SerializeField]
    private Vector3 initialPositionCharacter = new Vector3(0,0,0); //for the character
    [SerializeField]
    private Vector3 initialPositionCamera = new Vector3(0,0,0); //for the camera
    [SerializeField]
    private float zOffset = 1;  //for the character
    [SerializeField]
    private KeyCode changeModeKey = KeyCode.Q;
    
    public GameObject character;
    public GameObject camera;
    public UIManager uIManager;
    public SavePlayerMovements savePlayerMovements;
    public ReplayManager replayManager; 

    //false - camera mode, true - recording mode
    private bool gameMode;
    private bool canMove;
    private float currentTime = 0;


    public void Start()
    {
        character.SetActive(false);
        camera.SetActive(true);
        gameMode = false;
        canMove=false;
        uIManager.setLevelOverviewScreen();
    }


    public void Update()
    {
        if(Input.GetKeyDown(changeModeKey))
        {
            ChangeMode();
        }
    }


    public void setOverviewMode()
    {
        savePlayerMovements.StopRecording();
        replayManager.StopReplay();
        character.SetActive(false);
        camera.SetActive(true);
        gameMode = false;
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
            StartCoroutine(countdown());
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

        StartCoroutine(timer());
    }


    private IEnumerator<WaitForSeconds> timer()
    {
        currentTime=0;
        while (currentTime<maxTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime+=0.1f;
            uIManager.outputTimer(currentTime);
        }
    }
}