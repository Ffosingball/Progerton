using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerGame : MonoBehaviour
{
    public LevelManager levelManager;


    void OnRerecord_moves(InputValue value)
    {
        levelManager.handleRerecordMoves();
    }


    void OnSwitch_overview(InputValue value)
    {
        levelManager.ChangeMode();
    }


    void OnEnd_recording(InputValue value)
    {
        levelManager.handleEndRecording();
    }
}