using UnityEngine;

/*This class manages camera rotation of the character on the ground*/

public class PersonLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 2;
    [SerializeField]
    private float smoothing = 1.5f;

    public UIManager uIManager;
    public Transform character;

    private Vector2 velocity;
    private Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }


    void Update()
    {
        if(uIManager.getCursorlocked()) //Check that cursor is locked
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2();
            if (Cursor.lockState == CursorLockMode.Locked) // Только если мышь заблокирована
                mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            //Debug.Log("Rotate");
        }
    }
}
