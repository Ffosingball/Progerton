using System.Collections.Generic;
using UnityEngine;

/*This class replays movements in the lists*/

public class ReplayMovements : MonoBehaviour
{
    private List<Vector3> recordedPositions;
    private List<Quaternion> recordedQuaternions;
    private int currentIndex; //for the loop in the coroutune
    private SavePlayerMovements savePlayerMovements;
    private float yOffset, zOffset;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Coroutine replay=null;


    //getters
    public List<Vector3> getRecordedPositions(){return recordedPositions;}
    public List<Quaternion> getRecordedQuaternions(){return recordedQuaternions;}


    private void Start(){
        Reset();
    }


    //Gets data from the savePlayerMovements
    public void getData()
    {
        recordedPositions = savePlayerMovements.getAllPositions();
        recordedQuaternions = savePlayerMovements.getAllQuaternions();
    }


    public void StartReplaying()
    {
        if(replay == null)
        {
            //Debug.Log("Replay");
            replay = StartCoroutine(ReplayMovement());
        }
    }


    //Loop goes through all recorded data to replay movements
    //on the fixedUpdate speed
    private IEnumerator<WaitForFixedUpdate> ReplayMovement()
    {
        currentIndex=0;
        while (currentIndex < recordedPositions.Count)
        {
            Vector3 position = recordedPositions[currentIndex];
            position.y += yOffset;
            transform.position = position;
            transform.rotation = recordedQuaternions[currentIndex];
            currentIndex++;
            //Debug.Log("WTF");
            yield return new WaitForFixedUpdate(); // Повторяем с той же частотой
        }

        //When it finishes all recorded movements than just waits on the 
        //same place
        while(true)
        {
            yield return new WaitForFixedUpdate();
        }
    }


    public void StopReplaying(){
        if(replay!=null)
        {
            StopCoroutine(replay);
            replay=null;
            //Debug.Log("Stopped");
        }

        Reset();
    }


    //Sets position of the repeater to the start
    public void Reset()
    {
        Vector3 position = initialPosition;
        position.y += yOffset;
        position.z += zOffset;
        transform.position = position;
        transform.rotation = initialRotation;
    }
}