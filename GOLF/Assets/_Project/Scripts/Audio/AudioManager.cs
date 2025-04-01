using DG.Tweening;
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
        [SerializeField] private AudioSource _bgmAudioSource;

        public float CurrentVolume { get; private set; }

        public float CurrentMusicVolume => throw new NotImplementedException();

        private event Action<float> _onSfxChange; 
        public event Action<float> OnSfxChange
        {
            add
            {
                _onSfxChange += value;
            }

            remove
            {
                _onSfxChange -= value;
            }
        }

        private void Start()
        {
            LoadAudioSettings();
        }

        private void LoadAudioSettings()
        {
            float sfxVolume = SaveSystem.Source.GetSFXVolume();
            float musicVolume = SaveSystem.Source.GetMusicVolume();

            _sfxMixer.SetFloat("sfx_vol", sfxVolume);
            _musicMixer.SetFloat("music_vol", musicVolume);
        }

        public void SetSFXVolume(bool setVolumeUp, float volumeValue)
        {
            CurrentVolume = volumeValue;
            volumeValue = setVolumeUp ? Mathf.Max(-80, volumeValue - 20) : Mathf.Min(0, volumeValue + 20);
            _sfxMixer.SetFloat("sfx_vol", volumeValue);
            SaveSystem.Source.SetSFXVolume(volumeValue);
            _onSfxChange?.Invoke(volumeValue);
        }

        public void PlayLevelMusic(string audioName)
        {
            _bgmAudioSource.clip = _audioDatabase.GetAudio(audioName);
            _bgmAudioSource.Play();

            _musicMixer.SetFloat("bgm_vol", -80f);

            DOTween.To(
                () => {
                    _musicMixer.GetFloat("bgm_vol", out float currentVol);
                    return currentVol;
                },
                x => _musicMixer.SetFloat("bgm_vol", x),
                0,
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
                -80, 
                1
            );
        }

        public void BallHitSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("BallHitSFX"));
        public void PauseSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("PauseSFX"));
        public void ButtonClickSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonClickSFX"));
        public void ButtonSelectHoverSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonSelectHoverSFX"));
        public void SetAngleSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("SetAngleSFX"));

        public void SetMusicVolume(float volume)
        {
            throw new NotImplementedException();
        }
    }
}
