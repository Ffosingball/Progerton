using UnityEngine;

/*This class manages camera rotation of the character on the ground*/

public class PersonLook : MonoBehaviour
{
    [SerializeField]
    private float smoothing = 1.5f;

    public UIManager uIManager;
    public Transform character;
    public Movement movement;
    public CameraMovement cameraMovement;
    public SettingsManager settingsManager;
    //public LevelManager levelManager;

    private Vector2 velocity;
    private Vector2 frameVelocity;


    public void setVelocity(Vector2 velocity){this.velocity = velocity;}
    public void setFrameVelocity(Vector2 frameVelocity){this.frameVelocity = frameVelocity;}


    void Update()
    {
        if(uIManager.getCursorlocked()) //Check that cursor is locked
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2();
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                if(movement!=null)
                    mouseDelta = movement.getMouseInput();
                else
                    mouseDelta = cameraMovement.getMouseInput();
            }

            Vector2 rawFrameVelocity = mouseDelta * (settingsManager.getSettingsPreferences().sensitivity/10);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);
            //Debug.Log("Velocity: "+velocity);


            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            //Debug.Log(character.localRotation);
            //Debug.Log("Rotate");
        }
    }
}
