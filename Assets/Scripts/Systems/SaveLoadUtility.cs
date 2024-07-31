// File: Utils/SaveLoadUtility.cs
using System.IO;
using UnityEngine;

public static class SaveLoadUtility
{
    public static void SaveData<T>(T data, string filePath)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public static T LoadData<T>(string filePath)
    {
        if (!File.Exists(filePath))
            return default;

        string jsonData = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
