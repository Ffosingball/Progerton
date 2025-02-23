using System.Collections.Generic;
using UnityEngine;

public class ReplayMovements : MonoBehaviour
{
    private List<Vector3> recordedPositions;
    private List<Quaternion> recordedQuaternions;
    private int currentIndex;
    public SavePlayerMovements savePlayerMovements;
    public float yOffset=1f;
    public Vector3 initialPosition = new Vector3(0,0,0);
    private Coroutine replay=null;

    private void Start(){
        Vector3 position = initialPosition;
        position.y += yOffset;
        transform.position = position;
    }

    public void StartReplaying()
    {
        if(replay == null)
        {
            //Debug.Log("Replay");
            recordedPositions = savePlayerMovements.allPositions;
            recordedQuaternions = savePlayerMovements.allQuaternions;
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
        StopReplay();
    }

    public void StopReplay(){
        if(replay!=null)
        {
            StopCoroutine(replay);
            replay=null;
            //Debug.Log("Stopped");
        }
    }
}