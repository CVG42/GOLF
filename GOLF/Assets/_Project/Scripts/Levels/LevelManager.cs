using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

namespace Golf
{
    public class LevelManager : Singleton<ILevelSource>, ILevelSource
    {
        [SerializeField] private CanvasGroup _levelTransitionCanvasGroup;
        [SerializeField] private CanvasGroup _spawnTransitionCanvasGroup;
        [SerializeField] private GameObject _loadingBar;
        [SerializeField] private Transform[] _loadingBalls;

        private Sequence _currentTweenSequence;

        private async UniTask LoadSceneAsync(string sceneName)
        {
            _levelTransitionCanvasGroup.gameObject.SetActive(true);
            RotateBalls();
            _loadingBar.SetActive(true);
            await _levelTransitionCanvasGroup.DOFade(1, 2).AsyncWaitForCompletion();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
            _loadingBar.SetActive(false);
            await _levelTransitionCanvasGroup.DOFade(0, 1.5f).AsyncWaitForCompletion();
            _levelTransitionCanvasGroup.gameObject.SetActive(false);
            StopBalls();
        }

        public void LoadScene(string sceneName) 
        {
            LoadSceneAsync(sceneName).Forget();
        }

        public void TriggerSpawnTransition()
        {
            _spawnTransitionCanvasGroup.gameObject.SetActive(true);
            _currentTweenSequence?.Kill();
            _currentTweenSequence = DOTween.Sequence()
                .Append(_spawnTransitionCanvasGroup.transform.DOLocalMoveX(0, 1f, true))
                .AppendCallback(GameManager.Source.RespawnLastPosition)
                .Append(_spawnTransitionCanvasGroup.transform.DOLocalMoveX(1920, 1f))
                .AppendCallback(ResetTransitionPosition)
                .AppendCallback(() => _spawnTransitionCanvasGroup.gameObject.SetActive(false));
        }

        private void ResetTransitionPosition()
        {
            _spawnTransitionCanvasGroup.transform.position = new Vector2(-960, _spawnTransitionCanvasGroup.transform.position.y);
        }

        private void RotateBalls()
        {
            foreach (var ball in _loadingBalls)
            {
                ball.DORotate(new Vector3(0, 0, -360f), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            }
        }

        private void StopBalls()
        {
            foreach (var ball in _loadingBalls)
            {
                ball.DOKill();
            }
        }
    }
}
