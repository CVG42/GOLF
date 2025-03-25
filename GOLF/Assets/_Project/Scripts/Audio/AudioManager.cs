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

        public void BallHitSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("BallHitSFX"));
        public void PauseSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("PauseSFX"));
        public void ButtonClickSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonClickSFX"));
        public void ButtonSelectHoverSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonSelectHoverSFX"));
        public void SetAngleSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("SetAngleSFX"));
    }
}
