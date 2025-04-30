using UnityEngine;

namespace Golf
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            var source = LocalizationManager.Source as ILocalizationSource;
            return source != null ? source.GetLocalizedText(key) : key;
        }
    }
}
