using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Golf
{
    public class SaveSystem : Singleton<ISaveSource>, ISaveSource
    {
        private string _savePath;
        private GameData _currentData;

        protected override void Awake()
        {
            _savePath = Application.persistentDataPath + "/save.json";
            _currentData = Load();
        }

        private void Start()
        {
            GameData data = Load();
        }

        private void Save()
        {
            string json = JsonConvert.SerializeObject(_currentData, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        private void OnGameQuit()
        {
            Save();
        }

        private GameData Load()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                return JsonConvert.DeserializeObject<GameData>(json);
            }
            return new GameData();
        }

        public float GetMusicVolume() => _currentData.MusicVolume;
        public void SetMusicVolume(float volume)
        {
            _currentData.MusicVolume = volume;
            Save();
        }

        public float GetSFXVolume() => _currentData.SFXVolume;
        public void SetSFXVolume(float volume)
        {
            _currentData.SFXVolume = volume;
            Save();
        }
    }
}
