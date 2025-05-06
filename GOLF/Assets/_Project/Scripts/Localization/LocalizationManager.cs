using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Golf
{
    public class LocalizationManager : Singleton<ILocalizationSource>, ILocalizationSource
    {
        private Dictionary<string, Dictionary<string, string>> _localizationData = new();
        private string currentLanguage = "English";

        public event System.Action OnLanguageChanged;

        public static readonly string[] Languages = { "English", "Spanish", "Portuguese" };
        protected override void Awake()
        {
            base.Awake();
            LoadLocalizationData();
        }

        public void LoadLocalizationData()
        {
            _localizationData.Clear();

            string filePath = Path.Combine(Application.dataPath, "_Project/Localization/Localization.csv");
            if (!File.Exists(filePath))
            {
                Debug.LogError($"Localization file not found: {filePath}");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2) return;

            var headers = SplitCsvLine(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                var fields = SplitCsvLine(lines[i]);
                if (fields.Length != headers.Length) continue;

                string key = fields[0].Trim();
                var translations = new Dictionary<string, string>();

                for (int j = 1; j < headers.Length; j++)
                {
                    string lang = headers[j].Trim();
                    translations[lang] = fields[j];
                }

                _localizationData[key] = translations;
            }

            Debug.Log($"Loaded {_localizationData.Count} keys from CSV.");
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

        public void SetLanguage(string language)
        {
            if (currentLanguage == language) return;

            currentLanguage = language;
            OnLanguageChanged?.Invoke();
        }

        public string GetLocalizedText(string key)
        {
            if (_localizationData.TryGetValue(key, out var translations))
            {
                if (translations.TryGetValue(currentLanguage, out var value))
                    return value;
            }

            return key;
        }


        public string CurrentLanguage => currentLanguage;
    }
}
