using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Golf
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private async UniTask LoadSceneAsync(string sceneName)
        {
            _canvasGroup.gameObject.SetActive(true);
            await _canvasGroup.DOFade(1, 2).AsyncWaitForCompletion();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            await asyncOperation.ToUniTask();
            await _canvasGroup.DOFade(0, 1.5f).AsyncWaitForCompletion();
            _canvasGroup.gameObject.SetActive(false);
        }

        public void LoadScene(string sceneName) 
        {
            LoadSceneAsync(sceneName).Forget();
        }
    }
}
