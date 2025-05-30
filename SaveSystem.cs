using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveLevelData(LevelData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/listOfLevels2.omg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static LevelData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/listOfLevels2.omg";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("File does not exist "+path);
            return null;
        }
    }


    public static void SaveSettingsPreferences(SettingsPreferences data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/settingsPref4.omg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SettingsPreferences LoadSettingsPreferences()
    {
        string path = Application.persistentDataPath + "/settingsPref4.omg";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            SettingsPreferences data = formatter.Deserialize(stream) as SettingsPreferences;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("File does not exist "+path);
            return null;
        }
    }


    public static void SaveGameData(GameStatistics data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/gameStats.omg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static GameStatistics LoadGameData()
    {
        string path = Application.persistentDataPath + "/gameStats.omg";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            GameStatistics data = formatter.Deserialize(stream) as GameStatistics;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("File does not exist "+path);
            return null;
        }
    }


    public static OtherGameInfo LoadOtherGameData()
    {
        string path = Application.persistentDataPath + "/gameInfo.omg";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Read data from the file
            OtherGameInfo data = formatter.Deserialize(stream) as OtherGameInfo;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("File does not exist "+path);
            return null;
        }
    }
    

    public static void SaveOtherGameData(OtherGameInfo data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Create path to the file
        //Application.persistentDataPath is useful to create files for crossplatforms games
        //After add name and extension of the binary file
        string path = Application.persistentDataPath + "/gameInfo.omg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }
}