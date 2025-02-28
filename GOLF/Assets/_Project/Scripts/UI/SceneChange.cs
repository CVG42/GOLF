using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class SceneChange : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void ChangeScene()
        {
            LevelManager.Source.LoadScene(_sceneName);
        }
    }
}
