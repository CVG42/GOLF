using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class SaveSlotButton : MonoBehaviour
    {
        private const string DEFAULT_SCENE = "Tutorial";

        [SerializeField] private int _slotIndex;
        [SerializeField] private TextMeshProUGUI _slotName;
        [SerializeField] private TextMeshProUGUI _slotDescription;
        [SerializeField] private Button _slotButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private DeleteConfirmationPanel _deleteConfirmationPanel;

        private void Start()
        {
            _slotButton.onClick.AddListener(LoadGameData);
            _deleteButton.onClick.AddListener(DeleteGameFile);
            DisplaySlotData();
        }

        private void LoadGameData()
        {
            SaveSystem.Source.LoadGame(_slotIndex);
            LevelManager.Source.LoadScene(DEFAULT_SCENE);
        }

        private void DisplaySlotData()
        {
            string path = Application.persistentDataPath + $"/save{_slotIndex}.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                GameData data = JsonConvert.DeserializeObject<GameData>(json);

                int displayCurrentLevel = data.LastLevelCompleted + 1;

                _slotName.text = $"Slot {_slotIndex}";
                _slotDescription.text = $"Current Level: {displayCurrentLevel}";
            }
            else
            {
                _slotName.text = $"Empty Slot";
                _slotDescription.text = $"Current Level: --";
            }
        }

        private void DeleteGameFile()
        {
            _deleteConfirmationPanel.ShowConfirmationPanel(
                $"Are you sure you want to delete Slot {_slotIndex}?",
                () =>
                {
                    SaveSystem.Source.DeleteGameFile(_slotIndex);
                    DisplaySlotData();
                }
            );
        }
    }
}
