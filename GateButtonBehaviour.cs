using UnityEngine;

/*This class checks if someone inside the trigger*/

public class GateButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private int correspondingGate; //this field stores which flag in the array of all flags
    //this trigger changes

    public TriggersManager triggersManager;
    public Animator buttonAnimator; 


    void Start(){
        buttonAnimator.SetBool("pushed", false);
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setGate(correspondingGate, true);
            buttonAnimator.SetBool("pushed", true);
        }
    }

    public void disableTrigger()
    {
        buttonAnimator.SetBool("pushed", false);
        buttonAnimator.Play("button_off_idle_");
        triggersManager.setGate(correspondingGate, false);
    }
}