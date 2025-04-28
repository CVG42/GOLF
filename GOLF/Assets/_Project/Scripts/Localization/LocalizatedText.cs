using UnityEngine;
using TMPro;

namespace Golf
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizatedText : MonoBehaviour
    {
        [SerializeField] private string localizationKey;
        private TMP_Text textComponent;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();           
        }

        private void OnEnable()
        {
            LocalizationManager.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            LocalizationManager.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText()
        {
            if (textComponent != null && !string.IsNullOrEmpty(localizationKey))
            {
                textComponent.text = localizationKey.Localize();
            }
        }

        public void SetKey(string key)
        {
            localizationKey = key;
            UpdateText();
        }
    }
}
