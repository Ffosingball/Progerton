using UnityEngine;

public class GameTimeCounter : MonoBehaviour
{
    public static GameTimeCounter Instance;

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
        GameInfo.gameStatistics.timeSpentInGame+=Time.deltaTime;
        //Debug.Log(GameInfo.gameStatistics.timeSpentInGame);
    }
}