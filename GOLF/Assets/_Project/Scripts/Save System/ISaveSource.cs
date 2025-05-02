using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        void LoadGame(int gameIndex);
        void DeleteGameFile(int gameIndex);
        bool DoesFileExists(int gameIndex);
        GameData GetGameFileData(int gameIndex);
        GameData GetCurrentGameData();

        bool GetFullScreenMode();
        void SetFullScreenMode(bool isFullScreen);
        Vector2Int GetResolution();
        void SetResolution(int width, int height);

        float GetMusicVolume();
        void SetMusicVolume(float volume);
        float GetSFXVolume();
        void SetSFXVolume(float volume);

        bool IsLevelUnlocked(int levelID);
        void SetLevelCleared(int levelID);
        int GetHighestLevelCleared();

        void MarkTutorialAsCleared();
    }
}
