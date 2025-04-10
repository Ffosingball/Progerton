using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerGame : MonoBehaviour
{
    public LevelManager levelManager;


    void OnRerecord_moves(InputValue value)
    {
        if(levelManager.getCanMove())
            levelManager.handleRerecordMoves();
    }


    void OnSwitch_overview(InputValue value)
    {
        if(levelManager.getCanMove())
            levelManager.ChangeMode();
    }


    void OnEnd_recording(InputValue value)
    {
        if(levelManager.getCanMove())
            levelManager.handleEndRecording();
    }
}