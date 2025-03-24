using UnityEngine;

namespace Golf
{
    public interface IAudioSource
    {
        float CurrentVolume { get; }
        void SetSFXVolume(bool setVolumeUp);
        void PlayOneShot(string audioName);

        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void SetAngleSFX();
        void TypingSFX();
    }
}
