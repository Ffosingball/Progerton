using System.Collections.Generic;
using UnityEngine;

public class ReplayMovements : MonoBehaviour
{
    private List<Vector3> recordedPositions;
    private List<Quaternion> recordedQuaternions;
    private int currentIndex;
    public SavePlayerMovements savePlayerMovements;
    private Coroutine replay=null;

    public void StartReplaying()
    {
        if(replay == null)
        {
            Debug.Log("Replay");
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
            transform.position = recordedPositions[currentIndex];
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
            Debug.Log("Stopped");
        }
    }
}