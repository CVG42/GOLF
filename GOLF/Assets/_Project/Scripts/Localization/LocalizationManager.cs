using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Golf
{
    public class LocalizationManager : Singleton<ILocalizationSource>, ILocalizationSource
    {
        public static readonly string[] Languages = { "English", "Spanish", "Portuguese" };
        public string CurrentLanguage => _currentLanguage;
        public event System.Action OnLanguageChanged;

        private Dictionary<string, Dictionary<string, string>> _localizationData = new();
        private string _currentLanguage = "English";

        protected override void Awake()
        {
            base.Awake();
            LoadLocalizationData();
        }

        private void Start()
        {
            SetLanguage(SaveSystem.Source.GetCurrentLanguage());
        }

        public void LoadLocalizationData()
        {
            _localizationData.Clear();

            TextAsset filePath = Resources.Load<TextAsset>("Localization");
            if (filePath == null) return;

            var lines = filePath.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
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
            if (_currentLanguage == language) return;

            _currentLanguage = language;
            SaveSystem.Source.SetSelectedLanguage(_currentLanguage);
            OnLanguageChanged?.Invoke();
        }

        public string GetLocalizedText(string key)
        {
            if (_localizationData.TryGetValue(key, out var translations))
            {
                if (translations.TryGetValue(_currentLanguage, out var value))
                    return value;
            }

            return key;
        }
    }
}
