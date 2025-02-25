using System.Collections.Generic;
using UnityEngine;

public class SavePlayerMovements : MonoBehaviour
{
    public Transform player;
    public List<Vector3> allPositions;
    public List<Quaternion> allQuaternions;
    public bool isRecording=false;
    public ReplayManager replayManager;


    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRecording)
        {
            allPositions.Add(player.position);
            allQuaternions.Add(player.rotation);
        }
    }


    public void StartRecording(){
        isRecording = true;
        allPositions = new List<Vector3>();
        allQuaternions = new List<Quaternion>();
    }


    public void StopRecording(){
        isRecording = false;
        replayManager.resetData();
    }
}
