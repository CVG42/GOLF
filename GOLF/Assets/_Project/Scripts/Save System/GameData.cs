using System;

namespace Golf
{
    [Serializable]
    public class GameData
    {
        public int LastLevelCompleted = 0;
        public bool IsTutorialCleared = false;
        public int StrokesNumber = 50;
    }

    [Serializable]
    public class GameSettingsData
    {
        public bool IsFullScreen = true;
        public int ScreenWidth = 1920;
        public int ScreenHeight = 1080;
        public float MusicVolume = 1.0f;
        public float SFXVolume = 1.0f;
        public string Language = "English";
    }
}
