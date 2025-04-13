using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerGame : MonoBehaviour
{
    public LevelManager levelManager;
    public SoundManager soundManager;


    void OnRerecord_moves(InputValue value)
    {
        if(levelManager.getCanMove())
        {
            soundManager.playKeyPressedSound();
            levelManager.handleRerecordMoves();
        }
    }


    void OnSwitch_overview(InputValue value)
    {
        if(levelManager.getCanMove())
        {
            soundManager.playKeyPressedSound();
            levelManager.ChangeMode();
        }
    }


    void OnEnd_recording(InputValue value)
    {
        if(levelManager.getCanMove())
        {
            soundManager.playKeyPressedSound();
            levelManager.handleEndRecording();
        }
    }
}