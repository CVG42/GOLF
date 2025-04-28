using UnityEngine;

namespace Golf
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            if (LocalizationManager.Source == null)
            {
                Debug.LogWarning("LocalizationManager not found. Returning the key as fallback.");
                return key;
            }

            return LocalizationManager.Source.GetLocalizedText(key);
        }
    }
}
