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
        [SerializeField] private ConfirmationPanel _deleteConfirmationPanel;
        [SerializeField] private Sprite _activeButton;
        [SerializeField] private Sprite _inactiveButton;

        private ISaveSource _saveSystem;

        private void Awake()
        {
            _saveSystem = SaveSystem.Source;
        }

        private void Start()
        {
            _slotButton.onClick.AddListener(LoadGameData);
            _deleteButton.onClick.AddListener(DeleteGameFile);
            DisplaySlotData();
        }

        private void LoadGameData()
        {
            _saveSystem.LoadGame(_slotIndex);
            LevelManager.Source.LoadScene(DEFAULT_SCENE);
        }

        private void DisplaySlotData()
        {
            if (_saveSystem.DoesFileExists(_slotIndex))
            {
                GameData data = _saveSystem.GetGameFileData(_slotIndex);

                int displayCurrentLevel = data.LastLevelCompleted + 1;

                _deleteButton.interactable = true;
                _deleteButton.image.sprite = _activeButton;
                _slotName.text = $"Slot {_slotIndex}";
                _slotDescription.text = $"Current Level: {displayCurrentLevel}";
            }
            else
            {
                _deleteButton.interactable = false;
                _deleteButton.image.sprite = _inactiveButton;
                _slotName.text = "Empty Slot";
                _slotDescription.text = "Current Level: --";
            }
        }

        private void DeleteGameFile()
        {
            _deleteConfirmationPanel.ShowConfirmationPanel(OnConfirmationCallback);
        }

        private void OnConfirmationCallback()
        {
            _saveSystem.DeleteGameFile(_slotIndex);
            DisplaySlotData();
        }
    }
}
