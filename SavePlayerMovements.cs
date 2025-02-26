using System.Collections.Generic;
using UnityEngine;

public class SavePlayerMovements : MonoBehaviour
{
    public Transform player;
    public ReplayManager replayManager;

    private List<Vector3> allPositions;
    private List<Quaternion> allQuaternions;
    private bool isRecording=false;


    public void setAllPositions(List<Vector3> allPositions){this.allPositions = allPositions;}
    public void setAllQuaternions(List<Quaternion> allQuaternions){this.allQuaternions = allQuaternions;}
    public List<Vector3> getAllPositions(){return allPositions;}
    public List<Quaternion> getAllQuaternions(){return allQuaternions;}


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
