using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class RetryButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            GameManager.Source.RestoreHitsGameOver();
            UIManager.Source.HidePanels();
            LevelManager.Source.LoadScene(_sceneName);
        }
    }
}
