using System;

namespace Golf
{
    public interface IAudioSource
    {
        event Action<float> OnSFXVolumeChange;       
        event Action<float> OnMusicVolumeChange;       

        float CurrentSFXVolume { get; }
        float CurrentMusicVolume { get; }
        void SetSFXVolume(float volume);
        void SetMusicVolume(float volume);
        
        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void SetAngleSFX();
        void LoadAudioSettings();
    }
}
