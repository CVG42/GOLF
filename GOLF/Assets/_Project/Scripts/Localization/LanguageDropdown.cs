using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Golf
{
    public class LanguageDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

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
            UpdateDropdownLabels();
            dropdown.value = (int)LocalizationManager.Source.CurrentLanguage;
            dropdown.RefreshShownValue();
        }

        private void UpdateDropdownLabels()
        {
            int currentValue = dropdown.value;

            var languageKeys = new[] { "ENGLISH", "SPANISH", "PORTUGUESE" };

            var options = new List<TMP_Dropdown.OptionData>();
            foreach (string key in languageKeys)
            {
                options.Add(new TMP_Dropdown.OptionData(key.Localize()));
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(options);

            dropdown.value = currentValue;
            dropdown.RefreshShownValue();
        }



        private void OnLanguageSelected(int index)
        {
            LocalizationManager.Source.SetLanguage((Language)index);
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(OnLanguageSelected);
        }
    }
}
