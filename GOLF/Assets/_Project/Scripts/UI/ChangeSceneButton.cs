using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Golf
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(ChangeScene);
        }

        public void ChangeScene()
        {
            LevelManager.Source.LoadScene(_sceneName);
        }
    }
}
