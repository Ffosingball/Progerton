using UnityEngine;

public class BridgeBehaviour : MonoBehaviour
{
    [SerializeField]
    private int correspondingFlag = 0;

    public Animator bridgeAnimator; // Reference to Animator
    public TriggersManager triggersManager;

    private void FixedUpdate()
    {
        if (triggersManager.getFlag(correspondingFlag)) 
        {
            bridgeAnimator.SetBool("Appear", true); // Start appear animation
        }
        else
        {
            bridgeAnimator.SetBool("Appear", false); // Start disappear animation
        }


        if (triggersManager.getNextRound()) 
        {
            bridgeAnimator.Play("bridge_idle");
        }
    }
}
