using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Golf
{
    public class SaveSystem : Singleton<ISaveSource>, ISaveSource
    {
        private const string SAVE_FILE_NAME_FORMAT = "/save{0}.json";
        private const string SAVE_SETTINGS_NAME = "/settings.json";

        private string _saveSettingsPath;
        private string _saveGamePath;

        private GameSettingsData _currentSettingsData;
        private GameData _currentGameData;

        protected override void Awake()
        {
            base.Awake();

            LoadSettings();
        }

        private void OnGameQuit()
        {
            SaveSettings();
            SaveGame();
        }

        private void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(_currentSettingsData, Formatting.Indented);
            File.WriteAllText(_saveSettingsPath, json);
        }

        private void LoadSettings()
        {
            _saveSettingsPath = Application.persistentDataPath + SAVE_SETTINGS_NAME;
            if (File.Exists(_saveSettingsPath))
            {
                string json = File.ReadAllText(_saveSettingsPath);
                _currentSettingsData = JsonConvert.DeserializeObject<GameSettingsData>(json);
            }
            _currentSettingsData = new GameSettingsData();

            Screen.fullScreen = _currentSettingsData.IsFullScreen;

            if (!_currentSettingsData.IsFullScreen)
                Screen.SetResolution(_currentSettingsData.ScreenWidth, _currentSettingsData.ScreenHeight, false);
        }

        private void SaveGame()
        {
            string json = JsonConvert.SerializeObject(_currentGameData, Formatting.Indented);
            File.WriteAllText(_saveGamePath, json);
        }

        public void LoadGame(int gameIndex)
        {
            _saveGamePath = Application.persistentDataPath + string.Format(SAVE_FILE_NAME_FORMAT, gameIndex);
            if (File.Exists(_saveGamePath))
            {
                string json = File.ReadAllText(_saveGamePath);
                _currentGameData = JsonConvert.DeserializeObject<GameData>(json);
            }
            else
            {
                _currentGameData = new GameData();
                SaveGame();
            }
        }

        public void DeleteGameFile(int gameIndex)
        {
            string path = Application.persistentDataPath + string.Format(SAVE_FILE_NAME_FORMAT, gameIndex);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public bool IsLevelUnlocked(int levelID)
        {
            return _currentGameData.LastLevelCompleted >= levelID;
        }

        public int GetLevelCleared() => _currentGameData.LastLevelCompleted;

        public void SetLevelCleared(int levelID)
        {
            if (levelID > _currentGameData.LastLevelCompleted)
            {
                _currentGameData.LastLevelCompleted = levelID;
                SaveGame();
            }
        }

        public float GetMusicVolume() => _currentSettingsData.MusicVolume;
        public void SetMusicVolume(float volume)
        {
            _currentSettingsData.MusicVolume = volume;
            SaveSettings();
        }

        public float GetSFXVolume() => _currentSettingsData.SFXVolume;
        public void SetSFXVolume(float volume)
        {
            _currentSettingsData.SFXVolume = volume;
            SaveSettings();
        }

        public bool GetFullScreenMode() => _currentSettingsData.IsFullScreen;

        public void SetFullScreenMode(bool isFullScreen)
        {
            _currentSettingsData.IsFullScreen = isFullScreen;
            Screen.fullScreen = isFullScreen;
            SaveSettings();
        }

        public Vector2Int GetResolution() => new Vector2Int(_currentSettingsData.ScreenWidth, _currentSettingsData.ScreenHeight);

        public void SetResolution(int width, int height)
        {
            _currentSettingsData.ScreenWidth = width;
            _currentSettingsData.ScreenHeight = height;

            if (!Screen.fullScreen)
                Screen.SetResolution(width, height, false);

            SaveSettings();
        }        
    }
}
