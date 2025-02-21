using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public bool cursorlocked;
    public GameObject gameScreen, escapeScreen;
    public SavePlayerMovements savePlayerMovements;
    public TMP_Text isRecordingText;

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
            Cursor.lockState = CursorLockMode.None;
            cursorlocked=false;
            gameScreen.SetActive(false);
            escapeScreen.SetActive(true);
            Debug.Log("Open");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorlocked=true;
            escapeScreen.SetActive(false);
            gameScreen.SetActive(true);
            Debug.Log("Close");
        }

        if(savePlayerMovements.isRecording)
            isRecordingText.text = "Recording...";
        else
            isRecordingText.text = "...";
    }


    public void ReturnToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorlocked=true;
        escapeScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}
