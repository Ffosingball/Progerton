using UnityEngine;

/*This file controls gate animation*/

public class GateBehaviour : MonoBehaviour
{
    //Depending on which flag it should change its animations 
    [SerializeField]
    private int correspondingGate = 0;
    [SerializeField]
    private AudioClip gateSound;

    public Animator gateAnimator; // Reference to Animator
    public TriggersManager triggersManager;
    public AudioSource soundSource;

    private bool currentValue=false;

    private void FixedUpdate()
    {
        //Check changes
        if (triggersManager.getGate(correspondingGate) && triggersManager.getGate(correspondingGate)!=currentValue) 
        {
            //Change bool variable in animator
            gateAnimator.SetBool("Open", true); // Start open animation
            soundSource.PlayOneShot(gateSound);
            currentValue = true;
        }
        else if(triggersManager.getGate(correspondingGate)!=currentValue)
        {
            gateAnimator.SetBool("Open", false);
            gateAnimator.Play("gate_closed");
            currentValue = false;
        }


        if (triggersManager.getNextRound()) 
        {
            //call animation in animator by its name
            gateAnimator.SetBool("Open", false);
            gateAnimator.Play("gate_closed");
        }
    }
}