using UnityEngine;

/*This is a behaviour for the trigger which ends the game*/

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private Material triggerOn;  
    [SerializeField]
    private Material triggerOff;

    public TriggersManager triggersManager;


    void Start(){
        //Set its material
        GetComponent<Renderer>().material = triggerOff;
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setEndGame(true);
            GetComponent<Renderer>().material = triggerOn;
        }
    }
}
