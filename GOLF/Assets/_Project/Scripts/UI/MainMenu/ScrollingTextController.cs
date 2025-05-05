using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class ScrollingTextController : MonoBehaviour
    {
        [SerializeField] private RectTransform _textRectTransform;
        [SerializeField] private TextMeshProUGUI _textToScroll;
        [SerializeField] private float _scrollSpeed = 50f;

        private float _xPositionStart;
        private float _textBoxWidth;
        private Tween _scrollAnimationTween;

        void Start()
        {
            _xPositionStart = 0f;
            _textRectTransform.localPosition = new Vector3(0f, _textRectTransform.localPosition.y, _textRectTransform.localPosition.z);
        }

        public void SetScrollingText(string content, int amount)
        {
            string padded = string.Concat(Enumerable.Repeat(content + "     ", amount));

            _textToScroll.text = padded;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_textRectTransform);

            _textBoxWidth = _textToScroll.preferredWidth;

            _textRectTransform.localPosition = new Vector3(_xPositionStart, _textRectTransform.localPosition.y, _textRectTransform.localPosition.z);

            _scrollAnimationTween?.Kill();
            StartScrolling();
        }

        void StartScrolling()
        {
            _scrollAnimationTween = _textRectTransform.DOLocalMoveX(-_textBoxWidth, _textBoxWidth / _scrollSpeed)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .OnStepComplete(() =>
                {
                    _textRectTransform.localPosition = new Vector3(_xPositionStart, _textRectTransform.localPosition.y, _textRectTransform.localPosition.z);
                });
        }
    }
}
