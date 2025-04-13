using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerOverview : MonoBehaviour
{
    public LevelManager levelManager;
    public ReplayManager replayManager;
    public SoundManager soundManager;


    void OnReplay_moves(InputValue value)
    {
        soundManager.playKeyPressedSound();
        replayManager.StartReplay();
    }


    void OnStart_recording(InputValue value)
    {
        //soundManager.playKeyPressedSound();
        levelManager.ChangeMode();
    }


    void OnPrev_round(InputValue value)
    {
        soundManager.playKeyPressedSound();
        levelManager.handlePreviousRound();
    }
}
