using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf
{
    public class MenuButtonSelect : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private string _scrollingText;
        [SerializeField] private int _repeatedTextsAmount;
        [SerializeField] private ScrollingTextController _scrollingTextController;

        private bool _hasBeenSelected;

        public void OnSelect(BaseEventData eventData)
        {
            SetScrollingText();
        }

        void Update()
        {
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

        private void SetScrollingText()
        {
            _scrollingTextController.SetScrollingText(_scrollingText.Localize(), _repeatedTextsAmount);
        }
    }
}
