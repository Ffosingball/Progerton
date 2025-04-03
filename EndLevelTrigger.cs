using UnityEngine;

/*This is a behaviour for the trigger which ends the game*/

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // tag of object which we find

    public TriggersManager triggersManager;


    //This is called when some gameObject goes inside of the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check tag of the object
        {
            triggersManager.setEndGame(true);
        }
    }
}
