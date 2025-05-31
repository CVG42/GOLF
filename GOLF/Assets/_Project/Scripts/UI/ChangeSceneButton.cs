using UnityEngine;
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
            _button.interactable = true;
        }

        private void ChangeScene()
        {
            _button.interactable = false;
            LevelManager.Source.LoadScene(_sceneName);
        }
    }
}
