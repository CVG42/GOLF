using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        private const float MINIMUM_MIXER_VOLUME_VALUE = -80f;
        private const float MAXIMUM_MIXER_VOLUME_VALUE = 0f;

        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _sfxMixer;
        [SerializeField] private AudioMixer _musicMixer;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _bgmAudioSource;

        public float CurrentSFXVolume { get; private set; }

        public float CurrentMusicVolume { get; private set; }

        public event Action<float> OnSFXVolumeChange;
        public event Action<float> OnMusicVolumeChange;

        private void Start()
        {
            LoadAudioSettings();
        }

        private void LoadAudioSettings()
        {
            CurrentSFXVolume = SaveSystem.Source.GetSFXVolume();
            CurrentMusicVolume = SaveSystem.Source.GetMusicVolume();

            float sfxDb = Mathf.Log10(CurrentSFXVolume == 0f ? 0.0001f : CurrentSFXVolume) * 20f;
            float musicDb = Mathf.Log10(CurrentMusicVolume == 0f ? 0.0001f : CurrentMusicVolume) * 20f;

            _sfxMixer.SetFloat("sfx_vol", sfxDb);
            _musicMixer.SetFloat("bgm_vol", musicDb);
        }

        public void SetSFXVolume(float volume)
        {
            var volumeMixerValue = Mathf.Clamp01(volume);
            var dB = Mathf.Log10(volumeMixerValue == 0f ? 0.0001f : volumeMixerValue) * 20f;
            _sfxMixer.SetFloat("sfx_vol", dB);
            
            CurrentSFXVolume = volume;
            SaveSystem.Source.SetSFXVolume(volume);
            OnSFXVolumeChange?.Invoke(volume);
        }

        public void SetMusicVolume(float volume)
        {
            var volumeMixerValue = Mathf.Clamp01(volume);
            var dB = Mathf.Log10(volumeMixerValue == 0f ? 0.0001f : volumeMixerValue) * 20f;
            _musicMixer.SetFloat("bgm_vol", dB);
            
            CurrentMusicVolume = volume;
            SaveSystem.Source.SetMusicVolume(volume);
            OnMusicVolumeChange?.Invoke(volume);
        }

        public void PlayLevelMusic(string audioName)
        {
            _bgmAudioSource.clip = _audioDatabase.GetAudio(audioName);
            _bgmAudioSource.Play();

            _musicMixer.SetFloat("bgm_vol", MINIMUM_MIXER_VOLUME_VALUE);

            if(CurrentMusicVolume > 0)
            {
                DOTween.To(
                    () => {
                        _musicMixer.GetFloat("bgm_vol", out float currentVol);
                        return currentVol;
                    },
                    x => _musicMixer.SetFloat("bgm_vol", x),
                    MAXIMUM_MIXER_VOLUME_VALUE,
                    1
                );
            }
        }

        public void FadeOutMusic()
        {
            if (CurrentMusicVolume > 0) { 
                DOTween.To(
                    () => { 
                        _musicMixer.GetFloat("bgm_vol", out float currentVol); 
                        return currentVol; 
                    },
                    x => _musicMixer.SetFloat("bgm_vol", x), 
                    MINIMUM_MIXER_VOLUME_VALUE, 
                    1
                );
            }
        }

        public void PlayOneShot(string audioName)
        {
            _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio(audioName));
        }

        public void BallHitSFX() => PlayOneShot("BallHitSFX");

        public void PauseSFX() => PlayOneShot("PauseSFX");

        public void ButtonClickSFX() => PlayOneShot("ButtonClickSFX");

        public void ButtonSelectHoverSFX() => PlayOneShot("ButtonSelectHoverSFX");
      
        public void WaterPlatformBallSFX() => PlayOneShot("WaterPlatfomSFX");

        public void TypingSFX()
        {
            PlayOneShot(UnityEngine.Random.value >= 0.5 ? "Blip" : "Blop");
        }

        public void SandPlatformBallSFX() => PlayOneShot("SandPlatformSFX");

        public void SlimePlatformBallSFX() => PlayOneShot("SlimePlatformSFX");

        public void RockPlatformBallSFX() => PlayOneShot("RockPlatformSFX");

        public void IceWallBallSFX() => PlayOneShot("IceWallSFX");

        public void GrassPlatformBallSFX() => PlayOneShot("GrassPlatformSFX");

        public void HoleSFX() => PlayOneShot("HoleSFX");
    }
}
