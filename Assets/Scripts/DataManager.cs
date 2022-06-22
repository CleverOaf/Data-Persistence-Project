using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{
    public DataManager instance;

    private static string playerName;
    public static int highScore;
    public static string highScorePlayer;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
        LoadFile();
    }

    class SaveData
    {
        public int highScore;
        public string highScorePlayer;
    }

    public static void SaveFile()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorePlayer = highScorePlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScorePlayer = data.highScorePlayer;
        }
    }

    public void SwitchScene()
    {
        playerName = GameObject.Find("Name Input Field").GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }

    public static void SetHighScore(int points)
    {
        if (points > highScore)
        {
            highScore = points;
            highScorePlayer = playerName;
            SaveFile();
        }
    }
}
