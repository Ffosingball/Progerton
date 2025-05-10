using UnityEngine;
using UnityEngine.InputSystem;
using System;

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

    private Transform transform2;
    private Vector2 moveInput = new Vector2(0,0);
    private Vector2 mouseInput = new Vector2(0,0);
    private bool movesDownward=false, movesUpward=false;
    private Vector3 previousPosition;



    public Vector2 getMouseInput() {return mouseInput;}
    public void setMoveInput(Vector2 moveInput){this.moveInput = moveInput;}
    public void setPreviousPosition(Vector3 position){previousPosition = position;}



    void Awake()
    {
        // Get the rigidbody on this.
        transform2 = GetComponent<Transform>();
    }

    void Update()
    {
        float velocityY=0;

        //Check if player wants to move upward or downward
        if(movesUpward)
        {
            if(transform2.position.y<maxHeight)
                velocityY = verticalSpeed;
        }
        else if(movesDownward)
        {
            if(transform2.position.y>minHeight)
                velocityY = -verticalSpeed;
        }

        // Apply movement.
        transform2.Translate(new Vector3(moveInput.x* horizontalSpeed, velocityY, moveInput.y* horizontalSpeed)* Time.deltaTime);
        Vector3 pos = transform2.position;

        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform2.position = pos;

        float distanceWalked = (float)Math.Sqrt(Math.Pow((double)(transform.position.x-previousPosition.x),2)+Math.Pow((double)(transform.position.y-previousPosition.y),2)+Math.Pow((double)(transform.position.z-previousPosition.z),2));
        GameInfo.gameStatistics.distanceFlew+=distanceWalked;
        previousPosition = transform.position;
    }


    public void OnMove2(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }


    public void OnMove_downward(InputValue value)
    {
        if(value.isPressed && value.Get<float>()==1)
        {
            movesDownward=true;
        }
        else
        {
            movesDownward = false;
        }
    }


    public void OnMove_upward(InputValue value)
    {
        if(value.isPressed && value.Get<float>()==1)
        {
            movesUpward=true;
        }
        else
        {
            movesUpward = false;
        }
    }
}