using System;
using TMPro;
using UnityEngine;
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
            _confirmButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCancel);
            _confirmationText.text = _panelText;
        }

        public void ShowConfirmationPanel(Action confirm, Action cancel = null)
        {
            _onConfirm = confirm;
            _onCancel = cancel;
            gameObject.SetActive(true);
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
        }
    }
}
