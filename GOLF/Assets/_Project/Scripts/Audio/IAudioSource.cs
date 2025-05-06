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
        
        void PlayOneShot(string audioName);
        void PlayLevelMusic(string audioName);
        void FadeOutMusic();

        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void WaterPlatformBallSFX();
        void SandPlatformBallSFX();
        void SlimePlatformBallSFX();
        void RockPlatformBallSFX();
        void HoleSFX();
        void IceWallBallSFX();
        void GrassPlatformBallSFX();
        void TypingSFX();
    }
}
