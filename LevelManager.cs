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

    //0 - camera mode, 1 - recording mode
    private bool mode;
    private float currentTime = 0;


    //changes mode from recording to the camera
    public void ChangeMode(){}


    //starts countdown, then starts timer and recording
    public void StartRound(){}
}