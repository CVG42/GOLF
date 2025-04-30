using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Golf
{
    public class LocalizationManager : Singleton<ILocalizationSource>, ILocalizationSource
    {
        private Dictionary<string, Dictionary<Language, string>> _localizationData = new Dictionary<string, Dictionary<Language, string>>();
        private Language currentLanguage = Language.English;

        public event System.Action OnLanguageChanged;

        protected override void Awake()
        {
            base.Awake();
            LoadLocalizationData();
        }

        public void LoadLocalizationData()
        {
            _localizationData = new Dictionary<string, Dictionary<Language, string>>();
            string filePath = Path.Combine(Application.dataPath, "_Project/Localization/Localization.csv");

            if (!File.Exists(filePath))
            {
                Debug.LogError($"Localization file not found at path: {filePath}");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
            {
                Debug.LogError("Localization CSV does not have enough data.");
                return;
            }

            string[] headers = SplitCsvLine(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = SplitCsvLine(lines[i]);

                if (fields.Length != headers.Length)
                {
                    Debug.LogWarning($"Skipping malformed line {i + 1}: {lines[i]}");
                    continue;
                }

                string key = fields[0].Trim();
                var translations = new Dictionary<Language, string>();

                for (int j = 1; j < fields.Length; j++)
                {
                    if (TryParseLanguage(headers[j], out Language lang))
                    {
                        translations[lang] = fields[j];
                    }
                }

                _localizationData[key] = translations;
            }

            Debug.Log($"Loaded {_localizationData.Count} keys from Localization CSV");
        }

        private string[] SplitCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            string currentField = "";

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }
            fields.Add(currentField);

            return fields.ToArray();
        }

        private bool TryParseLanguage(string header, out Language language)
        {
            header = header.Trim().ToLower();

            switch (header)
            {
                case "english (en)":
                    language = Language.English;
                    return true;
                case "spanish (es-mx)":
                    language = Language.Spanish;
                    return true;
                case "portuguese (br)":
                    language = Language.Portuguese;
                    return true;
                default:
                    language = default;
                    return false;
            }
        }

        public void SetLanguage(Language language)
        {
            if (currentLanguage == language) return;

            currentLanguage = language;
            OnLanguageChanged?.Invoke();
        }

        public string GetLocalizedText(string key)
        {
            if (_localizationData.ContainsKey(key))
            {
                var translations = _localizationData[key];
                if (translations.ContainsKey(CurrentLanguage))
                    return translations[CurrentLanguage];
            }
            return key;
        }

        public Language CurrentLanguage => currentLanguage;
    }

    public enum Language
    {
        English,
        Spanish,
        Portuguese
    }
}
