using TMPro;
using UnityEngine;

namespace Golf
{
    public class UIManager : Singleton<IUISource>, IUISource
    {
        [SerializeField] private TextMeshProUGUI _strokesText;
        [SerializeField] private GameObject _losePanel;

        private void Start()
        {
            _losePanel.SetActive(false);
            UpdateHitsLeft(GameManager.Source.CurrentHitsLeft);
            
            GameManager.Source.OnHitsChanged += UpdateHitsLeft;
            GameManager.Source.OnLose += ShowLosePanel;
        }

        private void OnDestroy()
        {
            GameManager.Source.OnHitsChanged -= UpdateHitsLeft;
            GameManager.Source.OnLose -= ShowLosePanel;
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
    }
}