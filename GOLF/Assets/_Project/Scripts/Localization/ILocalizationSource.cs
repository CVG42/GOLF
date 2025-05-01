using UnityEngine;

namespace Golf
{
    public interface ILocalizationSource
    {
        string GetLocalizedText(string key);
        void SetLanguage(string language);
        string CurrentLanguage { get; }

        event System.Action OnLanguageChanged;
    }
}
