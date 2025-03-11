using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _sfxMixer;
        [SerializeField] private AudioSource _sfxAudioSource;

        public float CurrentVolume { get; private set; }

        private void Start()
        {
            GetAudioPlayerPrefKeys();
        }

        public void SetSFXVolume(bool setVolumeUp)
        {
            CurrentVolume = setVolumeUp ? Mathf.Max(-80, CurrentVolume - 20):Mathf.Min(0, CurrentVolume + 20);
            _sfxMixer.SetFloat("sfx_vol", CurrentVolume);
            PlayerPrefs.SetFloat("sfx_vol", CurrentVolume);
        }

        private void GetAudioPlayerPrefKeys()
        {
            if (PlayerPrefs.HasKey("sfx_vol"))
            {
                CurrentVolume = PlayerPrefs.GetFloat("sfx_vol", CurrentVolume);
                _sfxMixer.SetFloat("sfx_vol", CurrentVolume);
            }
            else CurrentVolume = 0;
        }

        public void BallHitSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("BallHitSFX"));

        public void PauseSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("PauseSFX"));

        public void ButtonClickSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonClickSFX"));

        public void ButtonSelectHoverSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonSelectHoverSFX"));
      
        public void SetAngleSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("SetAngleSFX"));       
    }
}
