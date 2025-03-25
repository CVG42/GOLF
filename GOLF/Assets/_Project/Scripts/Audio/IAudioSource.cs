using UnityEngine;

namespace Golf
{
    public interface IAudioSource
    {
        float CurrentVolume { get; }
        void SetSFXVolume(bool setVolumeUp);
        void FadeOutMusic();
        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void SetAngleSFX();
        void PlayLevelMusic(string audioName);
    }
}
