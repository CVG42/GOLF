using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class UIManager : Singleton<IUISource>, IUISource
    {
        [SerializeField] private TextMeshProUGUI _strokesText;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private Canvas _pausePanel;

        private void Start()
        {
            _losePanel.SetActive(false);
            UpdateHitsLeft(GameManager.Source.CurrentHitsLeft);
            
            GameManager.Source.OnHitsChanged += UpdateHitsLeft;
            GameManager.Source.OnLose += ShowLosePanel;
            GameStateManager.Source.OnGameStateChanged += OnGameStateChanged;
        }
        private void Update()
        {
            ActivatePausePanel();
        }

        private void OnDestroy()
        {
            GameManager.Source.OnHitsChanged -= UpdateHitsLeft;
            GameManager.Source.OnLose -= ShowLosePanel;
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
            _pausePanel.enabled = (newState == GameState.OnPlay);
        }

        public void ActivatePausePanel()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pausePanel.enabled = !_pausePanel.enabled; 
            }
        }

        public void DeactivatePausePanel()
        {
            _pausePanel.enabled = false;
        }

        public void RestartLevel()
        {
            OnGameStateChanged(GameState.OnPlay);

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}