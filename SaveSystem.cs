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
        string path = Application.persistentDataPath + "/listOfLevels.omg";
        //Create a file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Write data in that file
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static LevelData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/listOfLevels.omg";
        if(File.Exists(path))
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
}