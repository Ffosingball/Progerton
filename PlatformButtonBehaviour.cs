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
    private AudioClip buttonPush;

    public TriggersManager triggersManager;
    public Renderer renderer;
    public Animator buttonPlatformAnimator; 
    public AudioSource soundSource;

    private int numOfObjectsInside=0; //This is required for correct working of the trigger


    void Start(){
        renderer.material = triggerOff;
        buttonPlatformAnimator.SetBool("someoneInside", false);
    }


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            if(numOfObjectsInside==0)
                triggersManager.setActiveButton(1);

            triggersManager.setMovePlatforms(true);
            buttonPlatformAnimator.SetBool("someoneInside", true);
            renderer.material = triggerOn;
            numOfObjectsInside++;

            if(numOfObjectsInside==1)
                soundSource.PlayOneShot(buttonPush);
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
                renderer.material = triggerOff;
                buttonPlatformAnimator.SetBool("someoneInside", false);

                if(numOfObjectsInside==0)
                    triggersManager.setActiveButton(-1);
            }

            if(triggersManager.getActiveButton()==0)
                triggersManager.setMovePlatforms(false);
                soundSource.PlayOneShot(buttonPush);
        }
    }

    public void disableTrigger()
    {
        numOfObjectsInside=0;
        triggersManager.setMovePlatforms(false);
        renderer.material = triggerOff;
        buttonPlatformAnimator.SetBool("someoneInside", false);
    }
}