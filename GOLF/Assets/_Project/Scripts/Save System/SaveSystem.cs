using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Golf
{
    public class SaveSystem : Singleton<ISaveSource>, ISaveSource
    {
        private string _savePath;
        private GameData currentData;

        protected override void Awake()
        {
            _savePath = Application.persistentDataPath + "/save.json";
            currentData = Load();
        }

        private void Start()
        {
            GameData data = Load();
        }

        public void Save(GameData data)
        {
            currentData = data;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        private void SavePlayerData()
        {
            GameData data = new GameData
            {
            };
            Save(data);
        }

        private void OnGameQuit()
        {
            SavePlayerData();
        }

        public GameData Load()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                return JsonConvert.DeserializeObject<GameData>(json);
            }
            return new GameData { PlayerVolume = 1.0f };
        }

        public float GetVolume() => currentData.PlayerVolume;
        public void SetVolume(float volume) 
        {
            currentData.PlayerVolume = volume; 
            Save(currentData); 
        }
    }
}
