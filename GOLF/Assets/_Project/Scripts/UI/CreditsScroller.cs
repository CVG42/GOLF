using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Golf
{ 
    public class CreditsScroller : MonoBehaviour
    {
        [SerializeField] private RectTransform _textRectTransform;
        [SerializeField] private float _duration = 30f;
        [SerializeField] private float _initialPosition = -1000f;
        [SerializeField] private float _endPosition = 1000f;

        private void Start()
        {
            StartScrolling();
        }

        private void StartScrolling()
        {
            _textRectTransform
                .DOAnchorPosY(_endPosition, _duration)
                .From(new Vector2(_textRectTransform.anchoredPosition.x, _initialPosition))
                .SetEase(Ease.Linear)
                .OnComplete(() => WaitAfterCredits().Forget());           
        }

        private async UniTaskVoid WaitAfterCredits()
        {
            await UniTask.Delay(2000);
            LevelManager.Source.LoadScene("MainMenu");
        }
    }
}
