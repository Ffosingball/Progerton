using UnityEngine;

/*This class checks if someone inside the trigger*/

public class GateButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private int correspondingGate; //this field stores which flag in the array of all flags
    //this trigger changes
    [SerializeField]
    private AudioClip buttonPush;

    public TriggersManager triggersManager;
    public Animator buttonAnimator; 
    public AudioSource soundSource;


    private bool firstTime;


    void Start(){
        buttonAnimator.SetBool("pushed", false);
        firstTime = true;
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setGate(correspondingGate, true);
            buttonAnimator.SetBool("pushed", true);

            if(firstTime)
            {
                soundSource.PlayOneShot(buttonPush);
                firstTime = false;
            }
        }
    }


    public void disableTrigger()
    {
        buttonAnimator.SetBool("pushed", false);
        buttonAnimator.Play("button_off_idle_");
        //Debug.Log(""+correspondingGate);
        triggersManager.setGate(correspondingGate, false);
        firstTime = true;
    }
}