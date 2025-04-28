using UnityEngine;

namespace Golf
{
    public interface ILocalizationSource
    {
        void LoadLocalizationData();
        string GetLocalizedText(string key);
        void SetLanguage(Language language);
    }
}
