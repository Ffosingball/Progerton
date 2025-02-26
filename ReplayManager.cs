using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    [SerializeField]
    private int numOfRounds=3;
    [SerializeField]
    private float yOffset=1f;
    [SerializeField]
    private float zOffset=1f;
    [SerializeField]
    private Vector3 initialPosition = new Vector3(0,0,0); 
    [SerializeField]
    private Quaternion initialRotation;
    [SerializeField]
    private GameObject replayerPrefab;

    public SavePlayerMovements savePlayerMovements;
    public UIManager uIManager;

    private bool isReplaying=false;
    private int currentRound=-1;
    private List<GameObject> allReplayers;


    private void Start()
    {
        allReplayers = new List<GameObject>();
        NextRound();
    }


    public void NextRound()
    {
        if(isReplaying)
            StopReplay();

        currentRound++;

        if(currentRound==numOfRounds)
        {
            uIManager.OutputRoundStatus("This is the last Round!");
            currentRound--;
        }
        else
        {
            uIManager.OutputRoundNumber("Round "+currentRound);

            GameObject newReplay = Instantiate(replayerPrefab, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            ReplayMovements newRep = newReplay.GetComponent<ReplayMovements>();
            newRep.savePlayerMovements = savePlayerMovements;
            newRep.zOffset = zOffset*currentRound;
            newRep.yOffset = yOffset;
            newRep.initialPosition = initialPosition;
            newRep.initialRotation = initialRotation;

            allReplayers.Add(newReplay);
        }
    }


    public void PreviousRound()
    {
        if(isReplaying)
            StopReplay();

        currentRound--;

        if(currentRound==-1)
        {
            uIManager.OutputRoundStatus("This is the first Round!");
            currentRound++;
        }
        else
        {
            uIManager.OutputRoundNumber("Round "+currentRound);

            Destroy(allReplayers[currentRound+1]);
            allReplayers.RemoveAt(currentRound+1);

            ReplayMovements rep = allReplayers[currentRound].GetComponent<ReplayMovements>();
            savePlayerMovements.setAllPositions(rep.getRecordedPositions());
            savePlayerMovements.setAllQuaternions(rep.getRecordedQuaternions());
        }
    }


    public void StartReplay()
    {
        if(isReplaying)
            StopReplay();

        foreach(GameObject obj in allReplayers)
        {
            ReplayMovements rep = obj.GetComponent<ReplayMovements>();
            rep.StartReplaying();
        }

        isReplaying = true;
    }


    public void StopReplay()
    {
        foreach(GameObject obj in allReplayers)
        {
            ReplayMovements rep = obj.GetComponent<ReplayMovements>();
            rep.StopReplaying();
        }

        isReplaying = false;
    }


    public void resetData()
    {
        ReplayMovements newRep = allReplayers[currentRound].GetComponent<ReplayMovements>();
        newRep.getData();
    }
}