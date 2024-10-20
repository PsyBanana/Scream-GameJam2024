using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveLoadManager
{
    static readonly string _saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");

    public static async Task SaveGame(GameData gameData)
    {
        try
        {
            Dictionary<string, string> serializedData = new Dictionary<string, string>();

            foreach (var entry in gameData.Data)
            {
                string jsonValue = JsonUtility.ToJson(entry.Value);
                serializedData.Add(entry.Key, jsonValue);
            }

            string json = JsonUtility.ToJson(new SerializableDictionary(serializedData), true);

            using (StreamWriter writer = new StreamWriter(_saveFilePath))
            {
                await writer.WriteAsync(json);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
    }

    public static async Task<GameData> LoadGame()
    {
        if (!File.Exists(_saveFilePath))
        {
            return null;
        }

        try
        {
            using (StreamReader reader = new StreamReader(_saveFilePath))
            {
                string json = await reader.ReadToEndAsync();

                SerializableDictionary serializedData = JsonUtility.FromJson<SerializableDictionary>(json);

                GameData gameData = new GameData();
                foreach (var entry in serializedData.Data)
                {
                    object deserializedValue = JsonUtility.FromJson<object>(entry.Value);
                    gameData.SetData(entry.Key, deserializedValue);
                }
                return gameData;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load game: " + e.Message);
            return null;
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(_saveFilePath))
        {
            File.Delete(_saveFilePath);
        }
    }

    [System.Serializable]
    class SerializableDictionary
    {
        public Dictionary<string, string> Data;

        public SerializableDictionary(Dictionary<string, string> data)
        {
            Data = data;
        }
    }
}
