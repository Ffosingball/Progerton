using UnityEngine;

/*This class store flags of all triggers in the level*/

public class TriggersManager : MonoBehaviour
{
    [SerializeField]
    private int numOfTriggers = 3;
    private bool[] flags;


    //set all flags to false
    private void Start()
    {
        flags = new bool[numOfTriggers];

        foreach(bool flag in flags)
        {
            flag=false;
        }
    }


    //getters and setters for particular flag
    public bool getFlag(int index){return flags[index];}
    public void setFlag(int index, bool flag){flags[index]=flag;}

}