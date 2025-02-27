using System.Collections.Generic;
using UnityEngine;

/*This class records player movements and stores them*/

public class SavePlayerMovements : MonoBehaviour
{
    public Transform player;

    private List<Vector3> allPositions;
    private List<Quaternion> allQuaternions;
    private bool isRecording=false;


    //All getters and setters
    public void setAllPositions(List<Vector3> allPositions){this.allPositions = allPositions;}
    public void setAllQuaternions(List<Quaternion> allQuaternions){this.allQuaternions = allQuaternions;}
    public List<Vector3> getAllPositions(){return allPositions;}
    public List<Quaternion> getAllQuaternions(){return allQuaternions;}
    public bool getIsRecording(){return isRecording;}


    //Records position of the player with fixedUpdate speed
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
    }
}
