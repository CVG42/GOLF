using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class DeleteConfirmationPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _confirmationText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private Action _onConfirm;

        private void Start()
        {
            _confirmButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCancel);
        }

        public void ShowConfirmationPanel(string confirmationText, Action confirm)
        {
            _confirmationText.text = confirmationText;
            _onConfirm = confirm;
            gameObject.SetActive(true);
        }

        private void OnConfirm() 
        {
            _onConfirm?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnCancel() 
        { 
            gameObject.SetActive(false);
        }
    }
}
