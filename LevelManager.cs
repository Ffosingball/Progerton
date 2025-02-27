using UnityEngine;

/*This class manages all events and all non-movement
non-rotation inputs*/

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 20f; //Max time for one round
    [SerializeField]
    private Vector3 initialPositionCharacter = new Vector3(0,0,0); //for the character
    [SerializeField]
    private Vector3 initialPositionCamera = new Vector3(0,0,0); //for the camera
    [SerializeField]
    private float zOffset = 1;  //for the character
    [SerializeField]
    private KeyCode changeModeKey = KeyCode.Q;
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject camera;

    //false - camera mode, true - recording mode
    private bool gameMode;
    private float currentTime = 0;


    public void Start()
    {
        gameMode = true;
    }


    public void Update()
    {
        if(Input.GetKeyDown(changeModeKey))
        {
            ChangeMode();
        }
    }


    //changes mode from recording to the camera
    public void ChangeMode()
    {
        if(gameMode)
        {
            character.SetActive(false);
            camera.SetActive(true);
            gameMode = false;
        }
        else
        {
            camera.SetActive(false);
            character.SetActive(true);
            gameMode = true;
        }
    }


    //starts countdown, then starts timer and recording
    public void StartRound(){}
}