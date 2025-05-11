using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerGame : MonoBehaviour
{
    public LevelManager levelManager;
    public SoundManager soundManager;
    public UIManager uIManager;


    void OnRerecord_moves(InputValue value)
    {
        if(levelManager.getCanMove() && uIManager.getCursorlocked())
        {
            soundManager.playKeyPressedSound();
            levelManager.handleRerecordMoves();
        }
    }


    void OnSwitch_overview(InputValue value)
    {
        if(levelManager.getCanMove()  && uIManager.getCursorlocked())
        {
            soundManager.playKeyPressedSound();
            levelManager.ChangeMode();
        }
    }


    void OnEnd_recording(InputValue value)
    {
        if(levelManager.getCanMove()  && uIManager.getCursorlocked())
        {
            soundManager.playKeyPressedSound();
            levelManager.handleEndRecording();
        }
    }
}