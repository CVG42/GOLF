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
            {
                dropdown = GetComponent<TMP_Dropdown>();
            }

            if (dropdown == null)
            {
                Debug.LogError("Dropdown is not assigned and could not be found automatically.");
                return; 
            }

            InitializeDropdown();
            dropdown.onValueChanged.AddListener(OnLanguageSelected);
        }

        private void InitializeDropdown()
        {
            List<string> options = new List<string>();

            foreach (Language language in System.Enum.GetValues(typeof(Language)))
            {
                options.Add(language.ToString().Replace("_", " "));
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(options);

            dropdown.value = (int)LocalizationManager.Source.CurrentLanguage;
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
