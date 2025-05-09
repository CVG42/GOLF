using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf
{
    public class PlayButton : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private TMP_Text _playButtonText;
        [SerializeField] private int _repeatedTextsAmount;
        [SerializeField] private ScrollingTextController _scrollingTextController;

        private bool _hasBeenSelected;

        public void OnSelect(BaseEventData eventData)
        {
            SetScrollingText();
        }

        private void Start()
        {
            ChangePlayButtonText();
        }

        private void Update()
        {
            ChangePlayButtonText();

            if (!_hasBeenSelected && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                SetScrollingText();
                _hasBeenSelected = true;
            }
            else if (_hasBeenSelected && EventSystem.current.currentSelectedGameObject != gameObject)
            {
                _hasBeenSelected = false;
            }
        }

        private void ChangePlayButtonText()
        {
            if (SaveSystem.Source.DoesAnyFileExist())
            {
                _playButtonText.text = "Continue".Localize();
            }
            else
            {
                _playButtonText.text = "New Game".Localize();
            }
        }

        private void SetScrollingText()
        {
            string textToShow = SaveSystem.Source.DoesAnyFileExist() ? "Continue".Localize() : "New Game".Localize();
            _scrollingTextController.SetScrollingText(textToShow, _repeatedTextsAmount);
        }
    }
}
