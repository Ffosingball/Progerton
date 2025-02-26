using System.Collections.Generic;
using UnityEngine;

public class ReplayMovements : MonoBehaviour
{
    private List<Vector3> recordedPositions;
    private List<Quaternion> recordedQuaternions;
    private int currentIndex;
    private SavePlayerMovements savePlayerMovements;
    private float yOffset, zOffset;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Coroutine replay=null;

    public List<Vector3> getRecordedPositions(){return recordedPositions;}
    public List<Quaternion> getRecordedQuaternions(){return recordedQuaternions;}

    private void Start(){
        Reset();
    }

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

    public void Reset()
    {
        Vector3 position = initialPosition;
        position.y += yOffset;
        position.z += zOffset;
        transform.position = position;
        transform.rotation = initialRotation;
    }
}