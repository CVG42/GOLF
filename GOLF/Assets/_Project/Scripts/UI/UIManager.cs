using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Golf
{
    public class UIManager : Singleton<IUISource>, IUISource
    {
        [SerializeField] private TextMeshProUGUI _strokesText;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Canvas _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        private bool _isOnPlay;

        private void Start()
        {
            _losePanel.SetActive(false);
            UpdateHitsLeft(GameManager.Source.CurrentHitsLeft);
            
            GameManager.Source.OnHitsChanged += UpdateHitsLeft;
            GameManager.Source.OnLose += ShowLosePanel;
            InputManager.Source.OnPause += ActivatePausePanel;
            GameStateManager.Source.OnGameStateChanged += OnGameStateChanged;

            _resumeButton.onClick.AddListener(DeactivatePausePanel);
            _restartButton.onClick.AddListener(RestartLevel);
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
            }
        }

        public void DeactivatePausePanel()
        {
            _pausePanel.enabled = false;
            GameStateManager.Source.ChangeState(GameState.OnPlay);
        }

        public void RestartLevel()
        {
            GameStateManager.Source.ChangeState(GameState.OnPlay);

            _pausePanel.enabled = false;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}