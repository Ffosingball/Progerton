using UnityEngine;
using System.Collections.Generic;

/*This class store flags of all triggers in the level*/

public class TriggersManager : MonoBehaviour
{
    [SerializeField]
    private int numOfTriggers = 3;
    [SerializeField]
    private int numOfGates = 1;
    [SerializeField]
    private TriggerBehaviour[] triggers;
    [SerializeField]
    private GateButtonBehaviour[] gates;
    private bool[] flags, gatesMode;
    private bool endGame=false;
    private bool nextRound = false;


    //set all flags to false
    private void Start()
    {
        flags = new bool[numOfTriggers];
        gatesMode = new bool[numOfGates];

        for(int i = 0; i < flags.Length; i++)
        {
            flags[i]=false;
        }

        for(int i = 0; i < gatesMode.Length; i++)
        {
            gatesMode[i]=false;
        }
    }


    //getters and setters for particular flag
    public bool getFlag(int index){return flags[index];}
    public void setFlag(int index, bool flag){flags[index]=flag;}
    public bool getGate(int index){return gatesMode[index];}
    public void setGate(int index, bool gate){gatesMode[index]=gate;}
    public bool getEndGame(){return endGame;}
    public void setEndGame(bool endGame){this.endGame=endGame;}
    public bool getNextRound(){return nextRound;}


    public void disableAllTriggers()
    {
        foreach(TriggerBehaviour trigger in triggers)
        {
            trigger.disableTrigger();
        }

        foreach(GateButtonBehaviour gate in gates)
        {
            gate.disableTrigger();
        }
    }


    public void stopAllAnimations()
    {
        nextRound = true;
        StartCoroutine(wait());
    }


    private IEnumerator<WaitForSeconds> wait()
    {
        yield return new WaitForSeconds(0.2f);
         nextRound = false;
    }

}