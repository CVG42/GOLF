using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class ConfirmationPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _confirmationText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private string _panelText;

        private Action _onConfirm;
        private Action _onCancel;

        private void Start()
        {
            InputManager.Source.OnConfirmButtonPressed += OnOptionSelected;
            InputManager.Source.OnCancelButtonPressed += ClosePanel;

            _confirmButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCancel);
            _confirmationText.text = _panelText;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= OnOptionSelected;
            InputManager.Source.OnCancelButtonPressed -= ClosePanel;
        }

        private void ClosePanel()
        {
            if (!gameObject.activeInHierarchy) return;
            OnCancel();
        }

        private void OnOptionSelected()
        {
            if (!gameObject.activeInHierarchy) return;
            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected == _confirmButton.gameObject)
            {
                OnConfirm();
            }
            else
            {
                OnCancel();
            }
        }

        public void ShowConfirmationPanel(Action confirm, Action cancel = null)
        {
            _onConfirm = confirm;
            _onCancel = cancel;
            gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_confirmButton.gameObject);
        }

        private void OnConfirm() 
        {
            _onConfirm?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnCancel() 
        { 
            _onCancel?.Invoke();
            gameObject.SetActive(false);
            EventSystem.current.sendNavigationEvents = true;
        }
    }
}
