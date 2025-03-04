using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Golf
{
    public class LevelManager : Singleton<ILevelSource>, ILevelSource
    {
        [SerializeField] private CanvasGroup _canvasGroup;

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
    }
}
