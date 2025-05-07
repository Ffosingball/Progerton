using System.Collections.Generic;
using UnityEngine;

/*This class replays movements in the lists*/

public class ReplayMovements : MonoBehaviour
{
    public SavePlayerMovements savePlayerMovements;
    public Animator repeaterAnimator; 

    private List<Vector3> recordedPositions;
    private List<Quaternion> recordedQuaternions;
    private int currentIndex; //for the loop in the coroutune
    private float yOffset;
    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Coroutine replay=null;


    //getters
    public List<Vector3> getRecordedPositions(){return recordedPositions;}
    public List<Quaternion> getRecordedQuaternions(){return recordedQuaternions;}

    //setters
    public void setYOffset(float yOffset){this.yOffset=yOffset;}
    public void setInitialPosition(Vector3 initialPosition){this.initialPosition=initialPosition;}
    public void setInitialRotation(Vector3 initialRotation){this.initialRotation=initialRotation;}


    private void Start(){
        Reset();
        repeaterAnimator.SetBool("moves", false);
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
        if(recordedPositions!=null)
        {
            repeaterAnimator.SetBool("moves", true);
            currentIndex=0;
            while (currentIndex < recordedPositions.Count)
            {
                Vector3 position = recordedPositions[currentIndex];
                position.y += yOffset;
                if(transform.position == position)
                {
                    repeaterAnimator.SetBool("moves", false);
                }
                else
                {
                    repeaterAnimator.SetBool("moves", true);
                    transform.position = position;
                }

                transform.rotation = recordedQuaternions[currentIndex];
                currentIndex++;
                //Debug.Log("WTF");
                yield return new WaitForFixedUpdate(); // Повторяем с той же частотой
            }
        }

        repeaterAnimator.SetBool("moves", false);
        //Debug.Log("Finished");
        //When it finishes all recorded movements than just waits on the 
        //same place
        while(true)
        {
            yield return new WaitForFixedUpdate();
            //Debug.Log("Loop");
        }
    }


    public void StopReplaying(){
        if(replay!=null)
        {
            StopCoroutine(replay);
            repeaterAnimator.SetBool("moves", false);
            replay=null;
            //Debug.Log("Stopped");
        }

        Reset();
    }


    //Sets position of the repeater to the start
    public void Reset()
    {
        //Debug.Log("Reseted!");
        Vector3 position = initialPosition;
        position.y += yOffset;
        transform.position = position;
        transform.rotation = Quaternion.Euler(initialRotation);
    }
}