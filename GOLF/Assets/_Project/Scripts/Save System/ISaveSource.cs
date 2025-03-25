using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        float GetMusicVolume();
        void SetMusicVolume(float volume);

        float GetSFXVolume();
        void SetSFXVolume(float volume);
    }
}
