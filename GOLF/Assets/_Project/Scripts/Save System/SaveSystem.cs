using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Golf
{
    public class SaveSystem : MonoBehaviour, ISaveSource
    {
        private static SaveSystem _instance;
        private string _savePath;
        private GameData currentData;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _savePath = Application.persistentDataPath + "/save.json";
                currentData = Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Save(GameData data)
        {
            currentData = data;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        public GameData Load()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                return JsonConvert.DeserializeObject<GameData>(json);
            }
            return new GameData { _playerVolume = 1.0f };
        }

        public static SaveSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("SaveSystem instance is not initialized.");
                }
                return _instance;
            }
        }
        public int GetScore() => currentData._playerScore;
        public void SetScore(int score) { currentData._playerScore = score; Save(currentData); }

        public Vector2 GetPlayerPosition() => currentData._playerPosition;
        public void SetPlayerPosition(Vector2 position) { currentData._playerPosition = position; Save(currentData); }

        public float GetVolume() => currentData._playerVolume;
        public void SetVolume(float volume) { currentData._playerVolume = volume; Save(currentData); }
    }
}
