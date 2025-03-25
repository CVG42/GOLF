using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace Golf
{
    public class AudioManager : Singleton<IAudioSource>, IAudioSource
    {
        [SerializeField] private AudioDatabase _audioDatabase;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _bgmAudioSource;

        public float CurrentVolume { get; private set; }

        private void Start()
        {
            GetAudioPlayerPrefKeys();
        }

        public void SetSFXVolume(bool setVolumeUp)
        {
            CurrentVolume = setVolumeUp ? Mathf.Max(-80, CurrentVolume - 20):Mathf.Min(0, CurrentVolume + 20);
            _audioMixer.SetFloat("sfx_vol", CurrentVolume);
            PlayerPrefs.SetFloat("sfx_vol", CurrentVolume);
        }

        private void GetAudioPlayerPrefKeys()
        {
            if (PlayerPrefs.HasKey("sfx_vol"))
            {
                CurrentVolume = PlayerPrefs.GetFloat("sfx_vol", CurrentVolume);
                _audioMixer.SetFloat("sfx_vol", CurrentVolume);
            }
            else CurrentVolume = 0;
        }

        public void BallHitSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("BallHitSFX"));

        public void PauseSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("PauseSFX"));

        public void ButtonClickSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonClickSFX"));

        public void ButtonSelectHoverSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("ButtonSelectHoverSFX"));
      
        public void SetAngleSFX() => _sfxAudioSource.PlayOneShot(_audioDatabase.GetAudio("SetAngleSFX"));

        public void PlayLevelMusic(string audioName)
        {
            _bgmAudioSource.clip = _audioDatabase.GetAudio(audioName);
            _bgmAudioSource.Play();

            _audioMixer.SetFloat("bgm_vol", -80f);

            DOTween.To(
                () => {
                    _audioMixer.GetFloat("bgm_vol", out float currentVol);
                    return currentVol;
                },
                x => _audioMixer.SetFloat("bgm_vol", x),
                0,
                1
            );

        }

        public void FadeOutMusic()
        {
            DOTween.To(
                () => { 
                    _audioMixer.GetFloat("bgm_vol", out float currentVol); 
                    return currentVol; 
                },
                x => _audioMixer.SetFloat("bgm_vol", x), 
                -80, 
                1
            );
        }
    }
}
