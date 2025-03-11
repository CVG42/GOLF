using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class PlaySoundButton : MonoBehaviour
    {
        [SerializeField] private string _audioName;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(PlaySFX);
        }

        public void PlaySFX() => AudioManager.Source.ButtonClickSFX();
    }
}
