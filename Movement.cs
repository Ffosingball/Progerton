using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [Header("Running")]
    [SerializeField]
    private float runSpeed = 9;
    [SerializeField]
    private KeyCode runningKey = KeyCode.LeftShift;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    [SerializeField]
    private List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    
    public UIManager uIManager;


    public bool getIsRunning(){return IsRunning;}

    private Rigidbody rigidbody;
    private bool IsRunning { get; private set; }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(uIManager.getCursorlocked())
        {
            // Update IsRunning from input.
            IsRunning = Input.GetKey(runningKey);

            // Get targetMovingSpeed.
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
            {
                //Debug.Log("xyz: "+transform.position);
                targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            }

            // Get targetVelocity from input.
            Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

            // Apply movement.
            rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
        }
    }
}