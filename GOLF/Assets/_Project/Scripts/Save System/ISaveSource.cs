using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        float GetVolume();
        void SetVolume(float volume);

        bool GetFullScreenMode();
        void SetFullScreenMode(bool isFullScreen);

        Vector2Int GetResolution();
        void SetResolution(int width, int height);
    }
}
