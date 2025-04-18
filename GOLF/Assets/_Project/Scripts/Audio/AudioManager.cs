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

            var sfxVolumeMixerValue = Mathf.Lerp(MINIMUM_MIXER_VOLUME_VALUE, MAXIMUM_MIXER_VOLUME_VALUE, CurrentSFXVolume);
            var musicVolumeMixerValue = Mathf.Lerp(MINIMUM_MIXER_VOLUME_VALUE, MAXIMUM_MIXER_VOLUME_VALUE, CurrentMusicVolume);
            
            _sfxMixer.SetFloat("sfx_vol", sfxVolumeMixerValue);
            _musicMixer.SetFloat("music_vol", musicVolumeMixerValue);
        }

        public void SetSFXVolume(float volume)
        {
            var volumeMixerValue = Mathf.Lerp(MINIMUM_MIXER_VOLUME_VALUE, MAXIMUM_MIXER_VOLUME_VALUE, volume);
            _sfxMixer.SetFloat("sfx_vol", volumeMixerValue);
            
            CurrentSFXVolume = volume;
            SaveSystem.Source.SetSFXVolume(volume);
            OnSFXVolumeChange?.Invoke(volume);
        }

        public void SetMusicVolume(float volume)
        {
            var volumeMixerValue = Mathf.Lerp(MINIMUM_MIXER_VOLUME_VALUE, MAXIMUM_MIXER_VOLUME_VALUE, volume);
            _musicMixer.SetFloat("sfx_vol", volumeMixerValue);
            
            CurrentMusicVolume = volume;
            SaveSystem.Source.SetMusicVolume(volume);
            OnMusicVolumeChange?.Invoke(volume);
        }

        public void PlayLevelMusic(string audioName)
        {
            _bgmAudioSource.clip = _audioDatabase.GetAudio(audioName);
            _bgmAudioSource.Play();

            _musicMixer.SetFloat("bgm_vol", MINIMUM_MIXER_VOLUME_VALUE);

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

        public void FadeOutMusic()
        {
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

        public void PlayOneShot(string audioName)
        {
            _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio(audioName));
        }

        public void BallHitSFX() => PlayOneShot("BallHitSFX");

        public void PauseSFX() => PlayOneShot("PauseSFX");

        public void ButtonClickSFX() => PlayOneShot("ButtonClickSFX");

        public void ButtonSelectHoverSFX() => PlayOneShot("ButtonSelectHoverSFX");
      
        public void SetAngleSFX() => PlayOneShot("SetAngleSFX");

        public void TypingSFX()
        {
            PlayOneShot(UnityEngine.Random.value >= 0.5 ? "Blip" : "Blop");
        }
    }
}
