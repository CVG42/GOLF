using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class AudioSettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider sfxSlider;

        private void Awake()
        {
            sfxSlider = GetComponent<Slider>();
        }
        private void Start()
        {
            AudioManager.Source.OnSfxChange += SetSFXVolume;
            sfxSlider.value = AudioManager.Source.CurrentVolume;

            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        private void OnDestroy()
        {
            AudioManager.Source.OnSfxChange -= SetSFXVolume;
        }

        public void SetSFXVolume(float volume)
        {
            // codigo para actualizar slider 
            sfxSlider.value = volume;
        }
    }
}
