using UnityEngine;

/*This file controls bridge animation*/

public class BridgeBehaviour : MonoBehaviour
{
    //Depending on which flag it should change its animations 
    [SerializeField]
    private int correspondingFlag = 0;

    public Animator bridgeAnimator; // Reference to Animator
    public TriggersManager triggersManager;

    private void FixedUpdate()
    {
        //Check changes
        if (triggersManager.getFlag(correspondingFlag)) 
        {
            //Change bool variable in animator
            bridgeAnimator.SetBool("Appear", true); // Start appear animation
        }
        else
        {
            bridgeAnimator.SetBool("Appear", false); // Start disappear animation
        }


        if (triggersManager.getNextRound()) 
        {
            //call animation in animator by its name
            bridgeAnimator.Play("bridge_idle");
        }
    }
}
