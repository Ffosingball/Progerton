using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*This class manages all ui changes*/

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject escapeScreen;
    [SerializeField]
    private TMP_Text isRecordingText;
    [SerializeField]
    private TMP_Text lastFirstRoundText;
    [SerializeField]
    private TMP_Text roundText;

    public SavePlayerMovements savePlayerMovements;

    private bool cursorlocked;


    //Getter for cursorLocked
    public bool getCursorlocked(){return cursorlocked;}

    
    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && cursorlocked)//open pause menu
        {
            cursorlocked=false;
            Cursor.lockState = CursorLockMode.None;
            gameScreen.SetActive(false);
            escapeScreen.SetActive(true);
            //Debug.Log("Open");
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) //return to game
        {
            ReturnToGame();
        }

        //Check status and return
        if(savePlayerMovements.getIsRecording())
            isRecordingText.text = "Recording...";
        else
            isRecordingText.text = "...";
    }


    //Unpause the game
    public void ReturnToGame()
    {
        cursorlocked=true;
        Cursor.lockState = CursorLockMode.Locked;
        escapeScreen.SetActive(false);
        gameScreen.SetActive(true);
    }


    public void OutputRoundStatus(string output)
    {
        lastFirstRoundText.text = output;
    }


    public void OutputRoundNumber(string rText)
    {
        roundText.text = rText;
    }
}
