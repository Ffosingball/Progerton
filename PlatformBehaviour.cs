using System.Collections.Generic;
using UnityEngine;

/*This class manages movement of the platform*/

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float minHeight = -10;
    [SerializeField]
    private float maxHeight = 30;
    [SerializeField]
    private Vector3 initialPosition = new Vector3(0,0,0);
    [SerializeField]
    private bool directionUp = true;

    public TriggersManager triggersManager;
    public AudioSource soundSource;

    private Rigidbody rigidbody;
    private Transform transform;


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        resetPosition();
    }


    public void resetPosition()
    {
        rigidbody.linearVelocity = new Vector3(0,0,0);
        transform.position = initialPosition;
    }


    void Update()
    {
        if(triggersManager.getMovePlatforms())
        {
            if(directionUp)
            {
                rigidbody.linearVelocity = new Vector3(0,speed*Time.deltaTime,0);

                if(transform.position.y>maxHeight)
                    directionUp = false;
            }
            else
            {
                rigidbody.linearVelocity = new Vector3(0,-speed*Time.deltaTime,0);

                if(transform.position.y<minHeight)
                    directionUp = true;
            }

            if(!soundSource.isPlaying)
                soundSource.UnPause();
        }
        else
        {
            rigidbody.linearVelocity = new Vector3(0,0,0);

            if(soundSource.isPlaying)
                soundSource.Pause();
        }
    }
}