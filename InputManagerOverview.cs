using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerOverview : MonoBehaviour
{
    public LevelManager levelManager;
    public ReplayManager replayManager;
    public SoundManager soundManager;
    public UIManager uIManager;


    void OnReplay_moves(InputValue value)
    {
        if(uIManager.getCursorlocked())
        {
            soundManager.playKeyPressedSound();
            replayManager.StartReplay();
        }
    }


    void OnStart_recording(InputValue value)
    {
        //soundManager.playKeyPressedSound();
        if(uIManager.getCursorlocked())
        {
            levelManager.ChangeMode();
        }
    }


    void OnPrev_round(InputValue value)
    {
        if(uIManager.getCursorlocked())
        {
            soundManager.playKeyPressedSound();
            levelManager.handlePreviousRound();
        }
    }
}
