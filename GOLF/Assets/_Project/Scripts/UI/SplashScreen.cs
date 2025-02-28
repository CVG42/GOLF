using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Golf
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _logoImage;

        private async void Start()
        {
            await ShowSplashScreenAsync();
        }

        private async UniTask ShowSplashScreenAsync()
        {
            Sequence showLogo = DOTween.Sequence()
                .Append(_logoImage.DOFade(1, 1))
                .Join(_logoImage.transform.DOScale(8,1).SetEase(Ease.OutBounce))
                .AppendInterval(1.5f)
                .Append(_logoImage.DOFade(0, 1));
            await showLogo.AsyncWaitForCompletion();
            LevelManager.Source.LoadScene("MainMenu");
        }
    }
}
