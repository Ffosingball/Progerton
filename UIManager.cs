using UnityEngine;
using UnityEngine.UI;
using TMPro;


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

    public SavePlayerMovements savePlayerMovements;

    private bool cursorlocked;


    public bool getCursorlocked(){return cursorlocked;}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && cursorlocked)
        {
            cursorlocked=false;
            Cursor.lockState = CursorLockMode.None;
            gameScreen.SetActive(false);
            escapeScreen.SetActive(true);
            //Debug.Log("Open");
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            cursorlocked=true;
            Cursor.lockState = CursorLockMode.Locked;
            escapeScreen.SetActive(false);
            gameScreen.SetActive(true);
            //Debug.Log("Close");
        }

        if(savePlayerMovements.isRecording)
            isRecordingText.text = "Recording...";
        else
            isRecordingText.text = "...";
    }


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
}
