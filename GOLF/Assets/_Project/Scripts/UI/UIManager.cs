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
        [SerializeField] private Canvas _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _backToMenuButton;

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
            EventSystem.current.SetSelectedGameObject(_backToMenuButton.gameObject);
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