using UnityEngine;

/*This file controls gate animation*/

public class GateBehaviour : MonoBehaviour
{
    //Depending on which flag it should change its animations 
    [SerializeField]
    private int correspondingGate = 0;

    public Animator gateAnimator; // Reference to Animator
    public TriggersManager triggersManager;

    private void FixedUpdate()
    {
        //Check changes
        if (triggersManager.getGate(correspondingGate)) 
        {
            //Change bool variable in animator
            gateAnimator.SetBool("Open", true); // Start open animation
        }
        else
        {
            gateAnimator.Play("gate_closed");
        }


        if (triggersManager.getNextRound()) 
        {
            //call animation in animator by its name
            gateAnimator.Play("gate_closed");
        }
    }
}