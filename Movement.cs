using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*This class manages movement of the character on the ground*/

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float runSpeed = 9;
    [SerializeField]
    private float minYHeight = -10;
    [SerializeField]
    private float maxFallDistance = 8;
    [SerializeField]
    private GameObject redScreen, blackScreen, blueScreen;
    [SerializeField]
    private bool waterLevel = false;
    
    public UIManager uIManager;
    public LevelManager levelManager;
    public GroundCheck groundCheck;
    public SoundManager soundManager;

    private Rigidbody rigidbody;
    private Transform transform;
    private bool IsRunning;
    private Vector2 moveInput = new Vector2(0,0);
    private Vector2 mouseInput = new Vector2(0,0);
    private float lastYpositionOnGround;
    private Coroutine waitToEnd=null;
    private Vector3 externalVelocity = new Vector3(0,0,0);
    private Vector3 previousPosition;
    private GameObject currentFilterActive = null;


    //Getter for IsRunning
    public bool getIsRunning() { return IsRunning; }
    public Vector2 getMouseInput() {return mouseInput;}
    public void setMoveInput(Vector2 moveInput){this.moveInput = moveInput;}
    public void setLastPositionOnGround(float lastYpositionOnGround){this.lastYpositionOnGround=lastYpositionOnGround;}
    public void setPreviousPosition(Vector3 position){previousPosition = position;}


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            PlatformBehaviour platform = collision.gameObject.GetComponent<PlatformBehaviour>();
            if (platform != null)
            {
                // Apply only vertical platform velocity
                externalVelocity = new Vector3(0, platform.platformVelocity.y, 0);
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            externalVelocity = new Vector3(0,0,0);
            // Stop applying platform movement
        }
    }


    void FixedUpdate()
    {
        if(transform.position.y-lastYpositionOnGround<-maxFallDistance && waitToEnd==null)
        {
            if(groundCheck.isGrounded)
            {
                levelManager.setStopCounting(true);
                soundManager.playFallDamageSound();
                redScreen.SetActive(true);
                currentFilterActive = redScreen;
                waitToEnd = StartCoroutine(waitForRestart(2f));
            }
        }


        if(uIManager.getCursorlocked() && levelManager.getCanMove() && groundCheck.isGrounded)
        {
            float targetMovingSpeed;
            if (IsRunning)
                targetMovingSpeed = runSpeed;
            else
                targetMovingSpeed = speed;

            //rigidbody.linearVelocity = transform.rotation * new Vector3(moveInput.x* targetMovingSpeed, rigidbody.linearVelocity.y, moveInput.y* targetMovingSpeed);

            if(externalVelocity.y==0)
                rigidbody.linearVelocity = transform.rotation * new Vector3(moveInput.x* targetMovingSpeed, rigidbody.linearVelocity.y, moveInput.y* targetMovingSpeed);
            else
                rigidbody.linearVelocity = transform.rotation * new Vector3(moveInput.x* targetMovingSpeed, externalVelocity.y, moveInput.y* targetMovingSpeed);

            lastYpositionOnGround = transform.position.y;
        }

        //Debug.Log("Well: "+lastYpositionOnGround+"; dist: "+(transform.position.y-lastYpositionOnGround))

        /*if(groundCheck.isGrounded)
            lastYpositionOnGround = transform.position.y;*/

        if(transform.position.y<minYHeight && waitToEnd==null)
        {
            levelManager.setStopCounting(true);

            if(waterLevel)
            {
                soundManager.playFallUnderwaterSound();
                blueScreen.SetActive(true);
                currentFilterActive = blueScreen;
                waitToEnd = StartCoroutine(waitForRestart(3.5f));
            }
            else
            {
                soundManager.playfallBelowZeroSound();
                blackScreen.SetActive(true);
                currentFilterActive = blackScreen;
                waitToEnd = StartCoroutine(waitForRestart(2f));
            }
        }

        float distanceWalked = (float)Math.Sqrt(Math.Pow((double)(transform.position.x-previousPosition.x),2)+Math.Pow((double)(transform.position.y-previousPosition.y),2)+Math.Pow((double)(transform.position.z-previousPosition.z),2));
        GameInfo.gameStatistics.distanceWalked+=distanceWalked;
        previousPosition = transform.position;
    }


    private IEnumerator<WaitForSeconds> waitForRestart(float waitForSec)
    {
        levelManager.setCanMove(false);
        yield return new WaitForSeconds(waitForSec);
        levelManager.setStopCounting(false);
        currentFilterActive.SetActive(false);
        currentFilterActive = null;
        levelManager.setCanMove(true);
        waitToEnd = null;
        uIManager.yesRerecord();
        lastYpositionOnGround = transform.position.y;
    }


    public void dealWithFilter()
    {
        if (currentFilterActive != null)
        {
            if (currentFilterActive.activeSelf)
                currentFilterActive.SetActive(false);
            else
                currentFilterActive.SetActive(true);
        }    
    }


    public void OnSprint(InputValue value)
    {
        if (value.isPressed && value.Get<float>() == 1)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }
    }


    public void OnMove2(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }
}