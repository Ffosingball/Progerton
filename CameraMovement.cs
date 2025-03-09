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
    private float maxZ = 40;
    [SerializeField]
    private float minZ = -40;
    [SerializeField]
    private float maxX = 40;
    [SerializeField]
    private float minX = -40;
    [SerializeField]
    private KeyCode downwardKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode upwardKey = KeyCode.Space;

    private Transform transform2;



    void Awake()
    {
        // Get the rigidbody on this.
        transform2 = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float velocityY=0;

        //Check if player wants to move upward or downward
        if(Input.GetKey(upwardKey))
        {
            if(transform2.position.y<maxHeight)
                velocityY = verticalSpeed;
        }
        else if(Input.GetKey(downwardKey))
        {
            if(transform2.position.y>minHeight)
                velocityY = -verticalSpeed;
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * horizontalSpeed, Input.GetAxis("Vertical") * horizontalSpeed);

        // Apply movement.
        transform2.Translate(new Vector3(targetVelocity.x, velocityY, targetVelocity.y) * Time.deltaTime);
        Vector3 pos = transform2.position;

        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform2.position = pos;
    }
}