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
    private bool initDirectionUp;
    public Vector3 platformVelocity;


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        initDirectionUp = directionUp;
        resetPosition();
    }


    public void resetPosition()
    {
        transform.position = initialPosition;
        directionUp = initDirectionUp;
    }


    void FixedUpdate()
    {
        if(triggersManager.getMovePlatforms())
        {

            if(directionUp)
            {
                platformVelocity = new Vector3(0,speed,0);
                Vector3 newPosition = transform.position + new Vector3(0,speed * Time.fixedDeltaTime,0);
                rigidbody.MovePosition(newPosition);

                if(transform.position.y>maxHeight)
                    directionUp = false;
            }
            else
            {
                platformVelocity = new Vector3(0,-speed,0);
                Vector3 newPosition = transform.position + new Vector3(0,-speed * Time.fixedDeltaTime,0);
                rigidbody.MovePosition(newPosition);

                if(transform.position.y<minHeight)
                    directionUp = true;
            }

            if(!soundSource.isPlaying)
                soundSource.UnPause();
        }
        else
        {
            platformVelocity = new Vector3(0,0,0);


            if(soundSource.isPlaying)
                soundSource.Pause();
        }
    }
}