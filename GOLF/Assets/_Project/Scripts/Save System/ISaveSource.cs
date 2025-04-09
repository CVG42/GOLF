using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        bool GetFullScreenMode();
        void SetFullScreenMode(bool isFullScreen);
        Vector2Int GetResolution();
        void SetResolution(int width, int height);

        float GetMusicVolume();
        void SetMusicVolume(float volume);
        float GetSFXVolume();
        void SetSFXVolume(float volume);
    }
}
