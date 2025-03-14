using UnityEngine;
using DG.Tweening;

namespace Golf
{
    
    public class CreditsScroller : MonoBehaviour
    {
        [SerializeField] public RectTransform TextPosition;
        [SerializeField] private float _duration = 30f;
        [SerializeField] private float _initialPosition = -1000f;
        [SerializeField] private float _endPosition = 1000f;

        void Start()
        {
            StartScrolling();
        }

        void StartScrolling()
        {         
            TextPosition.anchoredPosition = new Vector2(TextPosition.anchoredPosition.x, _initialPosition);
            TextPosition.DOAnchorPosY(_endPosition, _duration)
                .SetEase(Ease.Linear)
                .OnComplete(StartScrolling);
        }
    }
}
