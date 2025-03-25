using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : Singleton<AudioManager>, IAudioSource
    {
        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _sfxMixer;
        [SerializeField] private AudioMixer _musicMixer;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _bgmAudioSource;

        public float CurrentVolume { get; private set; }

        private void Start()
        {
            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            float sfxVolume = SaveSystem.Source.GetSFXVolume();
            float musicVolume = SaveSystem.Source.GetMusicVolume();

            _sfxMixer.SetFloat("sfx_vol", sfxVolume);
            _musicMixer.SetFloat("music_vol", musicVolume);
        }

        public void SetSFXVolume(bool setVolumeUp)
        {
            CurrentVolume = setVolumeUp ? Mathf.Max(-80, CurrentVolume - 20) : Mathf.Min(0, CurrentVolume + 20);
            _sfxMixer.SetFloat("sfx_vol", CurrentVolume);
            SaveSystem.Source.SetSFXVolume(CurrentVolume);
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
    }
}
