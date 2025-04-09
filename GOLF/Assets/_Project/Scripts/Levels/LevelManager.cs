using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Golf
{
    public class LevelManager : Singleton<ILevelSource>, ILevelSource
    {
        [SerializeField] private CanvasGroup _levelTransitionCanvasGroup;
        [SerializeField] private CanvasGroup _spawnTransitionCanvasGroup;

        private Sequence _currentTweenSequence;

        private async UniTask LoadSceneAsync(string sceneName)
        {
            _levelTransitionCanvasGroup.gameObject.SetActive(true);
            await _levelTransitionCanvasGroup.DOFade(1, 2).AsyncWaitForCompletion();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
            await _levelTransitionCanvasGroup.DOFade(0, 1.5f).AsyncWaitForCompletion();
            _levelTransitionCanvasGroup.gameObject.SetActive(false);
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
                .AppendCallback(ResetTransitionPosition)
                .Append(_spawnTransitionCanvasGroup.transform.DOLocalMoveX(1920, 1f))
                .AppendCallback(() => _spawnTransitionCanvasGroup.gameObject.SetActive(false));
        }

        private void ResetTransitionPosition()
        {
            _spawnTransitionCanvasGroup.transform.position = new Vector2(-960, _spawnTransitionCanvasGroup.transform.position.y);
        }
    }
}
