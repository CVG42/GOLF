using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using static UnityEngine.Rendering.DebugUI;

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

            if (_hasBeenDestroyed) return;
            
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
            else
            {
                _currentSettingsData = new GameSettingsData();
            }

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
            _saveGamePath = GetFilePath(gameIndex);
            if (DoesFileExists(gameIndex))
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

        public bool DoesFileExists(int gameIndex)
        {
           return File.Exists(GetFilePath(gameIndex));
        }

        private string GetFilePath(int gameIndex)
        {
            return Application.persistentDataPath + string.Format(SAVE_FILE_NAME_FORMAT, gameIndex);
        }

        public GameData GetGameFileData(int gameIndex)
        {
            string path = GetFilePath(gameIndex);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<GameData>(json);
            }
            return null;
        }

        public bool DoesAnyFileExist()
        {
            string directory = Application.persistentDataPath;
            string[] savedFiles = Directory.GetFiles(directory, "save*.json");
            return savedFiles.Length > 0;
        }

        public void DeleteGameFile(int gameIndex)
        {
            if (DoesFileExists(gameIndex))
            {
                File.Delete(GetFilePath(gameIndex));
            }
        }

        public void MarkTutorialAsCleared()
        {
            _currentGameData.IsTutorialCleared = true;
            SaveGame();
        }

        public bool IsTutorialCleared()
        {
            return _currentGameData.IsTutorialCleared;
        }

        public bool IsLevelUnlocked(int levelID)
        {
            return _currentGameData.LastLevelCompleted >= levelID;
        }

        public int GetHighestLevelCleared() => _currentGameData.LastLevelCompleted;

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

        public string GetCurrentLanguage()
        {
            return _currentSettingsData.Language;
        }

        public void SetSelectedLanguage(string language)
        {
            _currentSettingsData.Language = language;
            SaveSettings();
        }

        public int GetStrokesNumber()
        {
            return _currentGameData.StrokesNumber;
        }

        public void SetStrokesNumber(int strokesNumber)
        {
            _currentGameData.StrokesNumber = strokesNumber;
            SaveGame();
        }
    }
}
