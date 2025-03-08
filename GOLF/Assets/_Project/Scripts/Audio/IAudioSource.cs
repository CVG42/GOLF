using UnityEngine;

namespace Golf
{
    public interface IAudioSource
    {
        float CurrentVolume { get; }
        void SetSFXVolume(bool volUp);
        void GetAudio(string audioName); // method just for testing
        void BallHitSFX();
        void ButtonClickSFX();
        void ButtonSelectHoverSFX();
        void PauseSFX();
        void SetAngleSFX();
    }
}
