using UnityEngine;

/*This class checks if someone inside the trigger*/

public class PlatformButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find
    [SerializeField]
    private Material triggerOn;  
    [SerializeField]
    private Material triggerOff;
    [SerializeField]

    public TriggersManager triggersManager;

    private int numOfObjectsInside=0; //This is required for correct working of the trigger


    void Start(){
        GetComponent<Renderer>().material = triggerOff;
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            if(numOfObjectsInside==0)
                triggersManager.setActiveButton(1);

            triggersManager.setMovePlatforms(true);
            GetComponent<Renderer>().material = triggerOn;
            numOfObjectsInside++;
        }
    }

    //This is called when some gameObject goes out of the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            numOfObjectsInside--;
            if (numOfObjectsInside == 0) //Check if somebody still there
            {
                GetComponent<Renderer>().material = triggerOff;

                if(numOfObjectsInside==0)
                    triggersManager.setActiveButton(-1);
            }

            if(triggersManager.getActiveButton()==0)
                triggersManager.setMovePlatforms(false);
        }
    }

    public void disableTrigger()
    {
        numOfObjectsInside=0;
        triggersManager.setMovePlatforms(false);
        GetComponent<Renderer>().material = triggerOff;
    }
}