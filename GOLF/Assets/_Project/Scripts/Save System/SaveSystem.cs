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

            Screen.fullScreen = _currentData.IsFullScreen;

            if (!_currentData.IsFullScreen)
                Screen.SetResolution(_currentData.ScreenWidth, _currentData.ScreenHeight, false);
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
            return new GameData { PlayerVolume = 1.0f };
        }

        public float GetVolume() => _currentData.PlayerVolume;
        public void SetVolume(float volume) 
        {
            _currentData.PlayerVolume = volume; 
            Save(); 
        }

        public bool GetFullScreenMode() => _currentData.IsFullScreen;

        public void SetFullScreenMode(bool isFullScreen)
        {
            _currentData.IsFullScreen = isFullScreen;
            Screen.fullScreen = isFullScreen;
            Save();
        }

        public Vector2Int GetResolution() => new Vector2Int(_currentData.ScreenWidth, _currentData.ScreenHeight);

        public void SetResolution(int width, int height)
        {
            _currentData.ScreenWidth = width;
            _currentData.ScreenHeight = height;

            if (!Screen.fullScreen)
                Screen.SetResolution(width, height, false);

            Save();
        }
    }
}
