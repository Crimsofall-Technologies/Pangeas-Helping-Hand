using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerDataSaver
{
    public static string filePath = Application.dataPath + "/player.data";

    public static void SaveData(PlayerData data)
    {
        try
        {
            // Create a BinaryFormatter and a FileStream to write the data
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
            Debug.Log("Player data saved successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save player data: " + ex.Message);
        }
    }

    public static PlayerData LoadData()
    {
        try
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Create a BinaryFormatter and a FileStream to read the data
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    PlayerData data = (PlayerData)formatter.Deserialize(stream);
                    Debug.Log("Player data loaded successfully.");
                    return data;
                }
            }
            else
            {
                Debug.LogWarning("Save file not found.");
                return null;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load player data: " + ex.Message);
            return null;
        }
    }
}
