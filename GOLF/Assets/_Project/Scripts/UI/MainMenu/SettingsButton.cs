using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private MenuTransitionController menuTransitionController;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            InputManager.Source.OnConfirmButtonPressed += ButtonSelected;

            _button.onClick.AddListener(() =>
            {
                ActivateSettingsPanel();
                EventSystem.current.SetSelectedGameObject(gameObject);
            });

            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= ButtonSelected;
        }

        private void ButtonSelected()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                _button.onClick.Invoke();
            }
        }

        private void ActivateSettingsPanel()
        {
            menuTransitionController?.ActivateSettingsPanel().Forget();
        }
    }
}
