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
            if (Random.value >= 0.5)
            {
                PlayOneShot("Blip");
            }
            else
            {
                PlayOneShot("Blop");
            }    
        }
    }
}
