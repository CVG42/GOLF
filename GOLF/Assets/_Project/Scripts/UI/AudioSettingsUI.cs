using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            musicSlider.value = SaveSystem.Source.GetMusicVolume();
            sfxSlider.value = SaveSystem.Source.GetSFXVolume();

            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        public void SetMusicVolume(float volume)
        {
            SaveSystem.Source.SetMusicVolume(volume);
            AudioManager.Source.LoadAudioSettings();
        }

        public void SetSFXVolume(float volume)
        {
            SaveSystem.Source.SetSFXVolume(volume);
            AudioManager.Source.LoadAudioSettings();
        }
    }
}
