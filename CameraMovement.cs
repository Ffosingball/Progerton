using UnityEngine;

/*This class manages movement of the camera*/

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeed = 9;
    [SerializeField]
    private float verticalSpeed = 5;
    [SerializeField]
    private float maxHeight = 40;
    [SerializeField]
    private float minHeight = 1;
    [SerializeField]
    private KeyCode downwardKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode upwardKey = KeyCode.Space;

    private Rigidbody rigidbody;



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float velocityY=0;

        //Check if player wants to move upward or downward
        if(Input.GetKey(upwardKey))
        {
            if(rigidbody.transform.position.y<maxHeight)
                velocityY = verticalSpeed;
        }
        else if(Input.GetKey(downwardKey))
        {
            if(rigidbody.transform.position.y>minHeight)
                velocityY = -verticalSpeed;
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * horizontalSpeed, Input.GetAxis("Vertical") * horizontalSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, velocityY, targetVelocity.y);
    }
}