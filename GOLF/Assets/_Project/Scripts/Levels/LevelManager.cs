using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Golf
{
    public class LevelManager : Singleton<ILevelSource>, ILevelSource
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private BallController _ballController;
        [SerializeField] private Transform _transition;

        private Sequence _currentTweenSequence;

        private async UniTask LoadSceneAsync(string sceneName)
        {
            _canvasGroup.gameObject.SetActive(true);
            await _canvasGroup.DOFade(1, 2).AsyncWaitForCompletion();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
            await _canvasGroup.DOFade(0, 1.5f).AsyncWaitForCompletion();
            _canvasGroup.gameObject.SetActive(false);
        }

        public void LoadScene(string sceneName) 
        {
            LoadSceneAsync(sceneName).Forget();
        }

        public void TriggerSpawnTransition()
        {
            _currentTweenSequence?.Kill();
            _currentTweenSequence = DOTween.Sequence()
                .Append(_transition.DOLocalMoveX(0, 1f, true))
                .AppendCallback(_ballController.ResetBallPosition)
                .Append(_transition.DOLocalMoveX(1920, 1f))
                .AppendCallback(ResetTransitionPosition);
        }
        private void ResetTransitionPosition()
        {
            _transition.transform.position = new Vector2(-960, _transition.transform.position.y);
        }

    }
}
