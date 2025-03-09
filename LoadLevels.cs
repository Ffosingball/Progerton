using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadLevels : MonoBehaviour
{
    [SerializeField]
    private GameObject levelButton;
    [SerializeField]
    private GameObject levelScreen;
    [SerializeField]
    private int tempNumOfLevels = 20;
    [SerializeField]
    private int sizeOfOneRow = 240;

    private LevelData data;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        for(int i=0; i<tempNumOfLevels; i++)
        {
            GameObject newBut = Instantiate(levelButton, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            newBut.transform.SetParent(levelScreen.transform);

            TMP_Text[] texts = newBut.GetComponentsInChildren<TMP_Text>();
            texts[0].text = data.levelNames[i];
            if(data.bestTime[i]==-1)
                texts[1].text = "";
            else
                texts[1].text = "best time: "+data.bestTime[i];

            // Get Button component and assign unique scene index
            Button button = newBut.GetComponent<Button>();
            int sceneIndex = i; // Store in local variable to prevent closure issue

            button.onClick.AddListener(() => ButtonClicked(sceneIndex));

            if(data.locked[i])
                button.interactable = false;
            else
                button.interactable = true;
        }
    }


    public void ButtonClicked(int num)
    {
        Debug.Log("Clicked: "+num);
        GameInfo.currentLevel = num;
        SceneManager.LoadScene(data.sceneName[num]);
    }


    public LevelData createFile()
    {
        data = new LevelData();
        List<string> levelNames = new List<string>();
        List<string> sceneName = new List<string>();
        List<bool> locked = new List<bool>();
        List<float> bestTime = new List<float>();
        List<int> pictureRef = new List<int>();

        for(int i=0; i<tempNumOfLevels; i++)
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
