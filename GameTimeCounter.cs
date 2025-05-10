using UnityEngine;

public class GameTimeCounter : MonoBehaviour
{
    public static GameTimeCounter Instance;

    public float totalTimeSpent = 0f;

    void Awake()
    {
        // Make this the only instance and don't destroy it between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Update()
    {
        totalTimeSpent += Time.deltaTime;
        GameInfo.gameStatistics.timeSpentInGame+=Time.deltaTime;
    }

    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(totalTimeSpent / 60f);
        int hoursRemainder = Mathf.FloorToInt(totalTimeSpent % 60f);
        int minutes = Mathf.FloorToInt(hoursRemainder / 60f);
        int seconds = Mathf.FloorToInt(hoursRemainder % 60f);
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}