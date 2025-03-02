using UnityEngine;

/*This class store flags of all triggers in the level*/

public class TriggersManager : MonoBehaviour
{
    [SerializeField]
    private int numOfTriggers = 3;
    [SerializeField]
    private TriggerBehaviour[] triggers;
    private bool[] flags;
    private bool endGame=false;


    //set all flags to false
    private void Start()
    {
        flags = new bool[numOfTriggers];

        for(int i = 0; i < flags.Length; i++)
        {
            flags[i]=false;
        }
    }


    //getters and setters for particular flag
    public bool getFlag(int index){return flags[index];}
    public void setFlag(int index, bool flag){flags[index]=flag;}
    public bool getEndGame(){return endGame;}
    public void setEndGame(bool endGame){this.endGame=endGame;}


    public void disableAllTriggers()
    {
        foreach(TriggerBehaviour trigger in triggers)
        {
            trigger.disableTrigger();
        }
    }

}