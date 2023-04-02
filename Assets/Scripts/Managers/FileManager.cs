using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public static class FileManager
{
    public static async UniTask<string> GetStageFileAsync(int i)
    {
        string filePath = Application.streamingAssetsPath + "/Stages/" + i + ".txt";
#if UNITY_EDITOR
        Debug.Log(filePath);
#endif

        string txt = (await UnityWebRequest.Get(filePath).SendWebRequest()).downloadHandler.text;
        return txt;
    }
    public static async UniTask<T> GetStageFileAsync<T>(int i)
    {
        string filePath = Application.streamingAssetsPath + "/Stages/" + i + ".json";
#if UNITY_EDITOR
        Debug.Log(filePath);
#endif

        string txt = (await UnityWebRequest.Get(filePath).SendWebRequest()).downloadHandler.text;
        T data = JsonUtility.FromJson<T>(txt);

        return data;
    }

    public static SaveData ReadSaveFile()
    {
        string filePath = Application.persistentDataPath + "/save.json";
#if UNITY_EDITOR
        Debug.Log(filePath);
#endif

        if (!File.Exists(filePath))
        {
#if UNITY_EDITOR
            Debug.Log("No file!");
#endif
            return new SaveData();
        }

        StreamReader saveFile = new StreamReader(filePath);

        SaveData data = JsonUtility.FromJson<SaveData>(saveFile.ReadToEnd());

        return data;
    }
    public static void WriteSaveFile(SaveData data)
    {
        string filePath = Application.persistentDataPath + "/save.json";

        StreamWriter saveFile = new StreamWriter(filePath);

        saveFile.Write(JsonUtility.ToJson(data));
        saveFile.Close();
    }
}