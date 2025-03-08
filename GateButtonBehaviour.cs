using UnityEngine;

/*This class checks if someone inside the trigger*/

public class GateButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private Material triggerOn;  
    [SerializeField]
    private Material triggerOff;
    [SerializeField]
    private int correspondingGate; //this field stores which flag in the array of all flags
    //this trigger changes

    public TriggersManager triggersManager;


    void Start(){
        GetComponent<Renderer>().material = triggerOff;
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setGate(correspondingGate, true);
            GetComponent<Renderer>().material = triggerOn;
        }
    }

    public void disableTrigger()
    {
        triggersManager.setGate(correspondingGate, false);
        GetComponent<Renderer>().material = triggerOff;
    }
}