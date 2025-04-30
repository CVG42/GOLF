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
            LocalizationManager.Source.OnLanguageChanged += UpdateDropdownLabels;
            dropdown.onValueChanged.AddListener(OnLanguageSelected);
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

            string languageListRaw = "LANGUAGE".Localize();
            string[] localizedLanguages = languageListRaw.Split(',');

            var options = new List<TMP_Dropdown.OptionData>();
            foreach (string lang in localizedLanguages)
            {
                options.Add(new TMP_Dropdown.OptionData(lang.Trim()));
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
            if (LocalizationManager.Source != null)
                LocalizationManager.Source.OnLanguageChanged -= UpdateDropdownLabels;
        }
    }
}
