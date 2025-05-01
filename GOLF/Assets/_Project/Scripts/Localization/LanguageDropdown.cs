using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Golf
{
    public class LanguageDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        private static readonly string[] languageKeys = { "English", "Spanish", "Portuguese" };

        private void Awake()
        {
            if (dropdown == null)
                dropdown = GetComponent<TMP_Dropdown>();

            if (dropdown == null)
            {
                Debug.LogError("Dropdown is not assigned and could not be found automatically.");
                return;
            }

            InitializeDropdown();
            dropdown.onValueChanged.AddListener(OnLanguageSelected);
        }

        private void OnEnable()
        {
            LocalizationManager.Source.OnLanguageChanged += UpdateDropdownLabels;
            UpdateDropdownLabels();
        }

        private void OnDisable()
        {
            LocalizationManager.Source.OnLanguageChanged -= UpdateDropdownLabels;
        }

        private void InitializeDropdown()
        {
            List<string> options = new List<string>();

            foreach (string key in languageKeys)
            {
                options.Add(key.Localize());
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(options);

            dropdown.value = GetCurrentLanguageIndex();
            dropdown.RefreshShownValue();
        }

        private void OnLanguageSelected(int index)
        {
            LocalizationManager.Source.SetLanguage(languageKeys[index]);
        }

        private void UpdateDropdownLabels()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var key in languageKeys)
                options.Add(new TMP_Dropdown.OptionData(key.Localize()));

            dropdown.options = options;
            dropdown.captionText.text = options[dropdown.value].text;
        }

        private int GetCurrentLanguageIndex()
        {
            string current = LocalizationManager.Source.CurrentLanguage;
            for (int i = 0; i < languageKeys.Length; i++)
            {
                if (languageKeys[i] == current)
                    return i;
            }
            return 0;
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(OnLanguageSelected);
        }
    }
}
