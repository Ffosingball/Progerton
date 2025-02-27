using System.Collections.Generic;
using UnityEngine;

/*This class manages all gameObjects which will
replay players movement in the level and rounds*/

public class ReplayManager : MonoBehaviour
{
    [SerializeField]
    private int numOfRounds=3;  //Max number of rounds
    [SerializeField]
    private float yOffset=1f;  //offset for the repeater 
    [SerializeField]
    private float zOffset=1f;  //ofsett for the initial position, so repeaters spawn next to each other
    [SerializeField]
    private Vector3 initialPosition = new Vector3(0,0,0); //Initial position for reapeter to appear
    [SerializeField]
    private Quaternion initialRotation = Quaternion.Euler(0, 0, 0);  //Initial rotation of repeater
    [SerializeField]
    private GameObject replayerPrefab;  //Prefab of the object which will replay players movements

    //References
    public SavePlayerMovements savePlayerMovements;
    public UIManager uIManager;

    private bool isReplaying=false;
    private int currentRound=-1;
    private List<GameObject> allReplayers; //List of all repeaters on the level


    private void Start()
    {
        allReplayers = new List<GameObject>();
        NextRound();
    }


    //This method starts new round and instantiates new repeater
    public void NextRound()
    {
        //Check if repeaters still replaying than stop replay
        //before go to the next round
        if(isReplaying)
            StopReplay();

        currentRound++;

        if(currentRound==numOfRounds)//Check if its the last round
        {
            uIManager.OutputRoundStatus("This is the last Round!");
            currentRound--;
        }
        else
        {
            if(currentRound!=0)//If its not the first round then 
            {
                //set repeater from previous true and reset its data
                allReplayers[currentRound-1].SetActive(true);
                resetData(currentRound-1);
            }

            uIManager.OutputRoundNumber("Round "+(currentRound+1));

            //Instantiate new repeater
            GameObject newReplay = Instantiate(replayerPrefab, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            //Get its logic and set all required fields
            ReplayMovements newRep = newReplay.GetComponent<ReplayMovements>();
            newRep.savePlayerMovements = savePlayerMovements;
            newRep.setZOffset(zOffset*currentRound);
            newRep.setYOffset(yOffset);
            newRep.setInitialPosition(initialPosition);
            newRep.setInitialRotation(initialRotation);

            newReplay.SetActive(false);

            //Add it to the list
            allReplayers.Add(newReplay);
        }
    }


    //This method returns back to the previous round and deletes last created repeater
    public void PreviousRound()
    {
        //Check if repeaters still replaying than stop replay
        //before go to the previous round
        if(isReplaying)
            StopReplay();

        currentRound--;

        //Check if its the first round
        if(currentRound==-1)
        {
            uIManager.OutputRoundStatus("This is the first Round!");
            currentRound++;
        }
        else
        {
            uIManager.OutputRoundNumber("Round "+(currentRound+1));

            //destroy last created repeater
            Destroy(allReplayers[currentRound+1]);
            allReplayers.RemoveAt(currentRound+1);

            //Set last recorded position and rotation to the savePlayerMovement
            //from the repeater in the current level
            ReplayMovements rep = allReplayers[currentRound].GetComponent<ReplayMovements>();
            savePlayerMovements.setAllPositions(rep.getRecordedPositions());
            savePlayerMovements.setAllQuaternions(rep.getRecordedQuaternions());

            //Hide the repeater of the current round 
            allReplayers[currentRound].SetActive(false);
        }
    }


    //start replay of all repeaters from the previous rounds
    public void StartReplay()
    {
        //Check if repeaters still replaying than stop replay
        //before start again
        if(isReplaying)
            StopReplay();

        //call replay for repeaters
        for(int i=0; i<currentRound; i++)
        {
            ReplayMovements rep = allReplayers[i].GetComponent<ReplayMovements>();
            rep.StartReplaying();
        }

        isReplaying = true;
    }


    //Stops all repetition
    public void StopReplay()
    {
        for(int i=0; i<currentRound; i++)
        {
            ReplayMovements rep = allReplayers[i].GetComponent<ReplayMovements>();
            rep.StopReplaying();
        }

        isReplaying = false;
    }


    //Sets new data to reapet for the gameobject at the index
    public void resetData(int index)
    {
        ReplayMovements newRep = allReplayers[index].GetComponent<ReplayMovements>();
        newRep.getData();
    }
}