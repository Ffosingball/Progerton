using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerOverview : MonoBehaviour
{
    public LevelManager levelManager;
    public ReplayManager replayManager;


    void OnReplay_moves(InputValue value)
    {
        replayManager.StartReplay();
    }


    void OnStart_recording(InputValue value)
    {
        levelManager.ChangeMode();
    }


    void OnPrev_round(InputValue value)
    {
        levelManager.handlePreviousRound();
    }
}
