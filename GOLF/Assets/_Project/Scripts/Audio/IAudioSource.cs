using System;
using UnityEngine;

namespace Golf
{
    public interface IAudioSource
    {
        event Action<float> OnSfxChange;       

        float CurrentVolume { get; }
        float CurrentMusicVolume { get; }
        void SetSFXVolume(bool setVolumeUp, float volumeValue);
        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void SetAngleSFX();
        void LoadAudioSettings();
        void SetMusicVolume(float volume);
    }
}
