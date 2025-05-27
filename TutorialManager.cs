using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private int numOfPages = 3;
    [SerializeField]
    private TMP_Text pageText, mainText;
    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;


    public SettingsManager settingsManager;


    private int currentPage;


    void Start()
    {
        if(GameInfo.otherGameInfo==null)
            GameInfo.getOtherInfo();
            
        GameInfo.otherGameInfo.startedTraining = true;
        currentPage = 0;
        updateText();
    }


    void Update()
    {
        // Left mouse button click (0)
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1f)
        {
            if (currentPage + 1 < numOfPages)
            {
                currentPage++;
                updateText();
            }

            if (currentPage + 1 == numOfPages)
            { 
                GameInfo.otherGameInfo.finishedTraining = true;
            }
        }

        // Right mouse button click (1)
        if (Input.GetMouseButtonDown(1) && Time.timeScale==1f)
        {
            if (currentPage - 1 >= 0)
            {
                currentPage--;
                updateText();
            }
        }

        if (settingsManager.getLanguageChanged())
        {
            updateText();
        }
    }


    private void updateText()
    {
        _currentStringTable = _localizedStringTable.GetTable();

        pageText.text = (currentPage + 1) +" "+ _currentStringTable["of"].LocalizedValue +" "+ numOfPages;
        mainText.text = _currentStringTable["p"+currentPage].LocalizedValue;
    }
}
