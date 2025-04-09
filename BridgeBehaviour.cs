using UnityEngine;

/*This file controls bridge animation*/

public class BridgeBehaviour : MonoBehaviour
{
    //Depending on which flag it should change its animations 
    [SerializeField]
    private int correspondingFlag = 0;
    [SerializeField]
    private AudioClip bridgeON, bridgeOFF;

    public Animator bridgeAnimator; // Reference to Animator
    public TriggersManager triggersManager;
    public AudioSource soundSource1, soundSource2;

    private bool currentValue=false;

    private void Update()
    {
        //Check changes
        if (triggersManager.getFlag(correspondingFlag) && triggersManager.getFlag(correspondingFlag)!=currentValue) 
        {
            //Change bool variable in animator
            bridgeAnimator.SetBool("Appear", true); // Start appear animation
            soundSource1.Stop();
            soundSource2.Stop();
            if (!triggersManager.getNextRound()) 
            {
                soundSource1.PlayOneShot(bridgeON);
                soundSource2.PlayOneShot(bridgeON);
            }
            currentValue = true;
        }
        else if(triggersManager.getFlag(correspondingFlag)!=currentValue)
        {
            bridgeAnimator.SetBool("Appear", false); // Start disappear animation
            soundSource1.Stop();
            soundSource2.Stop();
            if (!triggersManager.getNextRound()) 
            {
                soundSource1.PlayOneShot(bridgeOFF);
                soundSource2.PlayOneShot(bridgeOFF);
            }
            currentValue = false;
        }


        if (triggersManager.getNextRound()) 
        {
            //call animation in animator by its name
            bridgeAnimator.SetBool("Appear", false);
            bridgeAnimator.Play("bridge_idle");
        }
    }
}
