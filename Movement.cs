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
    private GameObject redScreen, blackScreen;
    
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


    //Getter for IsRunning
    public bool getIsRunning(){return IsRunning;}
    public Vector2 getMouseInput() {return mouseInput;}


    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        if(uIManager.getCursorlocked() && levelManager.getCanMove())
        {
            float targetMovingSpeed;
            if (IsRunning)
                targetMovingSpeed = runSpeed;
            else
                targetMovingSpeed = speed;

            // Apply movement.
            rigidbody.linearVelocity = transform.rotation * new Vector3(moveInput.x* targetMovingSpeed, rigidbody.linearVelocity.y, moveInput.y* targetMovingSpeed);
        }

        if(groundCheck.isGrounded && transform.position.y-lastYpositionOnGround<-maxFallDistance && waitToEnd==null)
        {
            levelManager.setStopCounting(true);
            soundManager.playFallDamageSound();
            redScreen.SetActive(true);
            waitToEnd = StartCoroutine(waitForRestart());
        }

        if(groundCheck.isGrounded)
            lastYpositionOnGround = transform.position.y;

        if(transform.position.y<minYHeight && waitToEnd==null)
        {
            levelManager.setStopCounting(true);
            soundManager.playfallBelowZeroSound();
            blackScreen.SetActive(true);
            waitToEnd = StartCoroutine(waitForRestart());
        }
    }


    private IEnumerator<WaitForSeconds> waitForRestart()
    {
        levelManager.setCanMove(false);
        yield return new WaitForSeconds(2f);
        levelManager.setStopCounting(false);
        redScreen.SetActive(false);
        blackScreen.SetActive(false);
        levelManager.setCanMove(true);
        waitToEnd = null;
        uIManager.yesRerecord();
    }


    public void OnSprint(InputValue value)
    {
        if(value.isPressed && value.Get<float>()==1)
        {
            IsRunning=true;
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