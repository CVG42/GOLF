using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class SlotButton : MonoBehaviour, ISelectHandler
    {
        private const string TUTORIAL_SCENE = "Tutorial";
        private const string HUB_SCENE = "LevelSelector";

        [SerializeField] private int _slotIndex;
        [SerializeField] private TextMeshProUGUI _slotName;
        [SerializeField] private TextMeshProUGUI _slotDescription;
        [SerializeField] private TextMeshProUGUI _slotHits;
        [SerializeField] private Button _slotButton;
        [SerializeField] private ConfirmationPanel _deleteConfirmationPanel;
        [SerializeField] private CanvasGroup _slotsCanvasGroup;

        private ISaveSource _saveSystem;
        private bool _isSelected = false;

        private void Awake()
        {
            _saveSystem = SaveSystem.Source;
        }

        private void Start()
        {
            InputManager.Source.OnDeleteButtonPressed += OnDeletePressed;

            _slotButton.interactable = true;
            _slotButton.onClick.AddListener(LoadGameData);
            DisplaySlotData();
        }

        private void OnDestroy()
        {
            InputManager.Source.OnDeleteButtonPressed -= OnDeletePressed;
        }

        private void OnDeletePressed()
        {
            if (!_isSelected) return;

            if (_saveSystem.DoesFileExists(_slotIndex))
            {              
                HandleDeleteRequest();
            }
        }

        private void HandleDeleteRequest()
        {
            _slotsCanvasGroup.interactable = false;
            _deleteConfirmationPanel.ShowConfirmationPanel(
                OnConfirmationCallback,
                () => 
                { 
                    EventSystem.current.SetSelectedGameObject(gameObject); 
                    _slotsCanvasGroup.interactable = true; 
                }
            );
        }

        private void LoadGameData()
        {
            _slotButton.interactable = false;
            _saveSystem.LoadGame(_slotIndex);
            if (!_saveSystem.IsTutorialCleared())
            {
                LevelManager.Source.LoadScene(TUTORIAL_SCENE);
            }
            else
            {
                LevelManager.Source.LoadScene(HUB_SCENE);
            }
        }

        private void DisplaySlotData()
        {
            if (_saveSystem.DoesFileExists(_slotIndex))
            {
                GameData data = _saveSystem.GetGameFileData(_slotIndex);

                int displayCurrentLevel = data.LastLevelCompleted + 1;

                _slotName.text = $"Slot {_slotIndex}";
                _slotDescription.text = "Current Level:".Localize() + " " + displayCurrentLevel;
                _slotHits.text = "Current Hits:".Localize() + " " + data.StrokesNumber;
            }
            else
            {
                _slotName.text = "New Game".Localize();
                _slotDescription.text = "Let's play some golf!".Localize();
                _slotHits.text = "";
            }
        }

        private void OnConfirmationCallback()
        {
            _saveSystem.DeleteGameFile(_slotIndex);
            DisplaySlotData();
            _slotsCanvasGroup.interactable = true;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _isSelected = true;
            DisplaySlotData();
        }

        private void OnDisable()
        {
            _isSelected = false;
        }
    }
}
