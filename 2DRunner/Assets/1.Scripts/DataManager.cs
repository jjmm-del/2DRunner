using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    
    public PlayerData CurrentPlayerData { get; private set; }

    private string _saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        _saveFilePath = Path.Combine(Application.persistentDataPath, "playerSaveData.json");
        LoadData();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(CurrentPlayerData, true);
        File.WriteAllText(_saveFilePath, jsonData);
        Debug.Log("데이터 성공적으로 저장:"+_saveFilePath);
    }

    public void LoadData()
    {
        if (File.Exists(_saveFilePath))
        {
            string jsonData = File.ReadAllText(_saveFilePath);
            CurrentPlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
            Debug.Log("저장된 데이터를 성공적으로 불러왔습니다.");
        }
        else
        {
            CurrentPlayerData = new PlayerData();
            SaveData();
            Debug.Log("저장된 파일이 없어 새로운 플레이어 데이터를 생성");
        }
    }
}
