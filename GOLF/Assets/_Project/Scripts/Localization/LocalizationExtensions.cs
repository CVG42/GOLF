namespace Golf
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            return LocalizationManager.Source.GetLocalizedText(key);
        }
    }
}
