using UnityEngine;
using TMPro;

namespace Golf
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string localizationKey;
        private TMP_Text textComponent;
        private ILocalizationSource localizationSource;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
            localizationSource = LocalizationManager.Source;
        }

        private void OnEnable()
        {
            if (localizationSource != null)
            {
                localizationSource.OnLanguageChanged += UpdateText;
                UpdateText();
            }
        }

        private void OnDisable()
        {
            if (localizationSource != null)
            {
                localizationSource.OnLanguageChanged -= UpdateText;
            }
        }

        private void UpdateText()
        {
            if (textComponent != null && !string.IsNullOrEmpty(localizationKey))
            {
                textComponent.text = localizationSource.GetLocalizedText(localizationKey);
            }
        }

        public void SetKey(string key)
        {
            localizationKey = key;
            UpdateText();
        }
    }
}
