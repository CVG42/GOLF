using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _sfxMixer;
        [SerializeField] private AudioMixer _musicMixer;
        [SerializeField] private AudioSource _sfxAudioSource;

        public float CurrentSFXVolume { get; private set; }

        public float CurrentMusicVolume { get; private set; }

        public event Action<float> OnSFXVolumeChange;
        public event Action<float> OnMusicVolumeChange;

        private void Start()
        {
            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            CurrentSFXVolume = SaveSystem.Source.GetSFXVolume();
            CurrentMusicVolume = SaveSystem.Source.GetMusicVolume();

            var sfxVolumeMixerValue = Mathf.Lerp(-80f, 0, CurrentSFXVolume);
            var musicVolumeMixerValue = Mathf.Lerp(-80f, 0, CurrentMusicVolume);
            
            _sfxMixer.SetFloat("sfx_vol", sfxVolumeMixerValue);
            _musicMixer.SetFloat("music_vol", musicVolumeMixerValue);
        }

        public void SetSFXVolume(float volume)
        {
            var volumeMixerValue = Mathf.Lerp(-80f, 0, volume);
            _sfxMixer.SetFloat("sfx_vol", volumeMixerValue);
            
            CurrentSFXVolume = volume;
            SaveSystem.Source.SetSFXVolume(volume);
            OnSFXVolumeChange?.Invoke(volume);
        }

        public void SetMusicVolume(float volume)
        {
            var volumeMixerValue = Mathf.Lerp(-80f, 0, volume);
            _musicMixer.SetFloat("sfx_vol", volumeMixerValue);
            
            CurrentMusicVolume = volume;
            SaveSystem.Source.SetMusicVolume(volume);
            OnMusicVolumeChange?.Invoke(volume);
        }

        public void BallHitSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("BallHitSFX"));
        public void PauseSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("PauseSFX"));
        public void ButtonClickSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonClickSFX"));
        public void ButtonSelectHoverSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonSelectHoverSFX"));
        public void SetAngleSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("SetAngleSFX"));
    }
}
