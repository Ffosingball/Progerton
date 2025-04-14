using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public class LoadLevels : MonoBehaviour
{
    [SerializeField]
    private GameObject levelButton;
    [SerializeField]
    private GameObject levelScreen;
    [SerializeField]
    private int tempNumOfLevels = 40, actualNumOfLevels = 4;
    [SerializeField]
    private int sizeOfOneRow = 240;

    [SerializeField]
    private LocalizedStringTable _localizedStringTable;
    private StringTable _currentStringTable;

    [SerializeField]
    private Sprite lockedLevelImage;
    [SerializeField]
    private Sprite[] unlockedLevelImage;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Image loadingFillBar;

    private LevelData data;
    private GameObject[] levelButtons;

    public SettingsManager settingsManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        levelButtons = new GameObject[tempNumOfLevels];

        RectTransform panel = levelScreen.GetComponent<RectTransform>();
        // Get the current size
        Vector2 size = panel.sizeDelta;
        // Change the height while keeping the width unchanged
        size.y = sizeOfOneRow*(tempNumOfLevels/4);
        // Apply the new size
        panel.sizeDelta = size;

        data = SaveSystem.LoadLevelData();
        if(data==null)
            data = createFile();

        // 2. Wait for the table to load asynchronously
        _currentStringTable = _localizedStringTable.GetTable();
        // At this point _currentStringTable can be used to
        // access our strings
        // 3. Retrieve the localized string
        string levelText = _currentStringTable["level"].LocalizedValue;
        string bestTimeText = _currentStringTable["best_time"].LocalizedValue;

        for(int i=0; i<actualNumOfLevels; i++)
        {
            GameObject newBut = Instantiate(levelButton, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            levelButtons[i] = newBut;
            newBut.transform.SetParent(levelScreen.transform);

            TMP_Text[] texts = newBut.GetComponentsInChildren<TMP_Text>();
            texts[0].text = levelText+" "+(i+1);
            if(data.bestTime[i]==-1)
                texts[1].text = "";
            else
                texts[1].text = bestTimeText+data.bestTime[i];

            // Get Button component and assign unique scene index
            Button button = newBut.GetComponent<Button>();
            int sceneIndex = i; // Store in local variable to prevent closure issue

            button.onClick.AddListener(() => ButtonClicked(sceneIndex));

            if(data.locked[i])
            {
                button.interactable = false;
                button.image.sprite = lockedLevelImage;
            }
            else
            {
                button.interactable = true;
                button.image.sprite = unlockedLevelImage[i];
            }

            RectTransform transform = newBut.GetComponent<RectTransform>();
            transform.localScale = new Vector3(1f,1f,1f);
        }

        //WHEN GAME IS COMPLETED DELETE THIS
        for(int i=actualNumOfLevels; i<tempNumOfLevels; i++)
        {
            GameObject newBut = Instantiate(levelButton, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            newBut.transform.SetParent(levelScreen.transform);

            TMP_Text[] texts = newBut.GetComponentsInChildren<TMP_Text>();
            texts[0].text = levelText+" "+(i+1);
            Button button = newBut.GetComponent<Button>();
            button.interactable = false;
            button.image.sprite = lockedLevelImage;

            RectTransform transform = newBut.GetComponent<RectTransform>();
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }


    private void Update()
    {
        if(settingsManager.getLanguageChanged())
        {
            for(int i=0; i<actualNumOfLevels; i++)
            {
                _currentStringTable = _localizedStringTable.GetTable();
                string levelText = _currentStringTable["level"].LocalizedValue;
                string bestTimeText = _currentStringTable["best_time"].LocalizedValue;

                TMP_Text[] texts = levelButtons[i].GetComponentsInChildren<TMP_Text>();
                texts[0].text = levelText+" "+(i+1);
                if(data.bestTime[i]==-1)
                    texts[1].text = "";
                else
                    texts[1].text = bestTimeText+data.bestTime[i];
            }
        }
    }


    public void ButtonClicked(int num)
    {
        //REMOVE IF WHEN GAME IS COMPLETE
        if(num!=0)
        {
            GameInfo.currentLevel = num;
            StartCoroutine(LoadSceneAsync(data.sceneName[num]));
        }
        else
            StartCoroutine(LoadSceneAsync("levelTemplate"));
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);

            loadingFillBar.fillAmount = progressValue;

            yield return null;
        }
    }


    public LevelData createFile()
    {
        data = new LevelData();
        List<string> levelNames = new List<string>();
        List<string> sceneName = new List<string>();
        List<bool> locked = new List<bool>();
        List<float> bestTime = new List<float>();
        List<int> pictureRef = new List<int>();

        for(int i=0; i<actualNumOfLevels; i++)
        {
            levelNames.Add("Level "+(i+1));
            sceneName.Add("level"+i);
            locked.Add(true);
            bestTime.Add(-1);
            pictureRef.Add(i);
        }
        locked[0] = false;

        data.levelNames = levelNames;
        data.sceneName = sceneName;
        data.locked = locked;
        data.bestTime = bestTime;
        data.pictureRef = pictureRef;

        SaveSystem.SaveLevelData(data);

        return data;
    }
}
