using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class AudioSettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        
        private void Start()
        {
            _sfxSlider.onValueChanged.AddListener(AudioManager.Source.SetSFXVolume);
            _musicSlider.onValueChanged.AddListener(AudioManager.Source.SetMusicVolume);
            LoadVolumeValues();
        }

        private void LoadVolumeValues()
        {
            _sfxSlider.value = AudioManager.Source.CurrentSFXVolume;
            _musicSlider.value = AudioManager.Source.CurrentMusicVolume;
        }
    }
}
