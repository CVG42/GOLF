using UnityEngine;
using UnityEngine.EventSystems;

namespace Golf
{
    public class MenuButtonSelect : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private string _scrollingText;
        [SerializeField] private int _repeatedTextsAmount;
        [SerializeField] private ScrollingTextController _scrollingTextController;

        public void OnSelect(BaseEventData eventData)
        {
            _scrollingTextController.SetScrollingText(_scrollingText.Localize(), _repeatedTextsAmount);
        }
    }
}
