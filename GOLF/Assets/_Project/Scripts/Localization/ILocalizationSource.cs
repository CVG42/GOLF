using UnityEngine;

namespace Golf
{
    public interface ILocalizationSource
    {
        string GetLocalizedText(string key);
        void SetLanguage(Language language);
        Language CurrentLanguage { get; }

        event System.Action OnLanguageChanged;
    }
}
