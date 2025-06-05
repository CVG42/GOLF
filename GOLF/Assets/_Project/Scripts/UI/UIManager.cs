using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Golf
{
    public class UIManager : Singleton<IUISource>, IUISource
    {
        [SerializeField] private TextMeshProUGUI _strokesText;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Canvas _settingsPanel;
        [SerializeField] private Canvas _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Slider _settingsFirstButton;
        [SerializeField] private Button _closeSettingsButton;

        private bool _isOnPlay;

        private void Start()
        {
            _losePanel.SetActive(false);
            UpdateHitsLeft(SaveSystem.Source.GetStrokesNumber());
            
            GameManager.Source.OnHitsChanged += UpdateHitsLeft;
            GameManager.Source.OnLose += ShowLosePanel;
            InputManager.Source.OnPause += ActivatePausePanel;
            GameStateManager.Source.OnGameStateChanged += OnGameStateChanged;

            _resumeButton.onClick.AddListener(DeactivatePausePanel);
            _settingsButton.onClick.AddListener(ShowSettingsPanel);
            _closeSettingsButton.onClick.AddListener(HideSettingsPanel);
            _mainMenuButton.onClick.AddListener(BackToMainMenu);
        }

        private void OnDestroy()
        {
            GameManager.Source.OnHitsChanged -= UpdateHitsLeft;
            GameManager.Source.OnLose -= ShowLosePanel;
            InputManager.Source.OnPause -= ActivatePausePanel;
            GameStateManager.Source.OnGameStateChanged -= OnGameStateChanged;
        }

        public void UpdateHitsLeft(int hitsLeft)
        {
            _strokesText.text = "Strokes left: " + hitsLeft;
        }

        public void ShowLosePanel()
        {
            _losePanel.SetActive(true);
            GameStateManager.Source.ChangeState(GameState.OnGameOver);
            EventSystem.current.SetSelectedGameObject(_backToMenuButton.gameObject);
        }

        public void ShowSettingsPanel()
        {
            _pausePanel.enabled = false;
            _settingsPanel.enabled = true;
            EventSystem.current.SetSelectedGameObject(_settingsFirstButton.gameObject);
        }

        public void HideSettingsPanel()
        {
            _settingsPanel.enabled = false;
            _pausePanel.enabled = true;
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
        }

        private void BackToMainMenu()
        {
            LevelManager.Source.LoadScene("MainMenu");
        }

        public void HidePanels()
        {
            _losePanel.SetActive(false);
        }

        public void OnGameStateChanged(GameState newState)
        {
            _isOnPlay = newState == GameState.OnPlay;
        }

        public void ActivatePausePanel()
        {
            if (_isOnPlay) 
            {
                _pausePanel.enabled = false;
            }
            else if (!_isOnPlay)
            {
                _pausePanel.enabled = true;
                EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
            }
        }

        public void DeactivatePausePanel()
        {
            _pausePanel.enabled = false;
            GameStateManager.Source.ChangeState(GameState.OnPlay);
        }
    }
}