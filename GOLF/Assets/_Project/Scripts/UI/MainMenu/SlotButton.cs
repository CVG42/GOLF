using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class SlotButton : MonoBehaviour, ISelectHandler
    {
        private const string DEFAULT_SCENE = "Tutorial";

        [SerializeField] private int _slotIndex;
        [SerializeField] private TextMeshProUGUI _slotName;
        [SerializeField] private TextMeshProUGUI _slotDescription;
        [SerializeField] private Button _slotButton;
        [SerializeField] private ConfirmationPanel _deleteConfirmationPanel;

        private ISaveSource _saveSystem;
        private bool _isSelected = false;

        private void Awake()
        {
            _saveSystem = SaveSystem.Source;
        }

        private void Start()
        {
            InputManager.Source.OnDeleteButtonPressed += OnDeletePressed;
            InputManager.Source.OnConfirmButtonPressed += OnSelectSlot;

            _slotButton.onClick.AddListener(() =>
            {
                LoadGameData();
                EventSystem.current.SetSelectedGameObject(gameObject);
            });
            DisplaySlotData();
        }

        private void OnDestroy()
        {
            InputManager.Source.OnDeleteButtonPressed -= OnDeletePressed;
            InputManager.Source.OnConfirmButtonPressed -= OnSelectSlot;
        }

        private void OnSelectSlot()
        {
            if (!_isSelected) return;

            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                _slotButton.onClick.Invoke();
            }
        }

        private void OnDeletePressed()
        {
            if (!_isSelected) return;

            if (_saveSystem.DoesFileExists(_slotIndex))
            {
                HandleDeleteRequest().Forget();
            }
        }

        private async UniTaskVoid HandleDeleteRequest()
        {
            await UniTask.NextFrame();

            _deleteConfirmationPanel.ShowConfirmationPanel(OnConfirmationCallback, () =>
            {
                UniTask.Void(async () =>
                {
                    await UniTask.NextFrame();
                    EventSystem.current.SetSelectedGameObject(gameObject);
                });
            });
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

                _slotName.text = $"Slot {_slotIndex}";
                _slotDescription.text = $"Current Level: {displayCurrentLevel}";
            }
            else
            {
                _slotName.text = "New Game";
                _slotDescription.text = "Let's play some golf!";
            }
        }

        private void OnConfirmationCallback()
        {
            _saveSystem.DeleteGameFile(_slotIndex);
            DisplaySlotData();
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
