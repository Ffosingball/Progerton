using UnityEngine;

/*This class checks if someone inside the trigger*/

public class TriggerBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private Material triggerOn;  
    [SerializeField]
    private Material triggerOff;
    [SerializeField]
    private int correspondingFlag; //this field stores which flag in the array of all flags
    //this trigger changes
    [SerializeField]
    private AudioClip leverPull;

    public TriggersManager triggersManager;
    public Renderer renderer;
    public Animator triggerAnimator; 
    public AudioSource soundSource;

    private int numOfObjectsInside=0; //This is required for correct working of the trigger


    void Start(){
        renderer.material = triggerOff;
        triggerAnimator.SetBool("isOn", false);
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered");
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setFlag(correspondingFlag, true);
            renderer.material = triggerOn;
            triggerAnimator.SetBool("isOn", true);
            numOfObjectsInside++;

            if(numOfObjectsInside==1)
                soundSource.PlayOneShot(leverPull);
        }
    }

    //This is called when some gameObject goes out of the trigger
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited");
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            numOfObjectsInside--;
            if (numOfObjectsInside == 0) //Check if somebody still there
            {
                triggersManager.setFlag(correspondingFlag, false);
                renderer.material = triggerOff;
                triggerAnimator.SetBool("isOn", false);
                soundSource.PlayOneShot(leverPull);
            }
        }
    }

    public void disableTrigger()
    {
        //Debug.Log("Disabled");
        numOfObjectsInside=0;
        triggersManager.setFlag(correspondingFlag, false);
        renderer.material = triggerOff;
        triggerAnimator.SetBool("isOn", false);
        //soundSource.PlayOneShot(leverPull);
    }
}
