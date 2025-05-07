using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Golf
{
    public class MenuTransitionController : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private RectTransform _mainMenuSidebar;
        [SerializeField] private Button[] _mainMenuButtons;
        [SerializeField] private RectTransform _saveSlotsSidebar;
        [SerializeField] private Button[] _saveSlotsButtons;
        [SerializeField] private VerticalLayoutGroup _mainMenuSidebarLayoutGroup;

        [Header("DOTween Parameters")]
        [SerializeField] private float _animationSpeed = 0.4f;
        [SerializeField] private float _buttonsDisplacement = 500f;
        [SerializeField] private float _sidebarDisplacement = 300f;

        [Header("Save slots panel")]
        [SerializeField] private RectTransform _slotInfoPanel;
        [SerializeField] private Vector2 _slotInfoPanelFinalPosition;
        [SerializeField] private float _slotInfoPanelOffsetPosition = 800f;
        [SerializeField] private float _slotInfoPanelExitDisplacement = 50f;
        [SerializeField] private float _startTime = 0.6f;
        [SerializeField] private float _slotInfoPanelExitDuration = 0.4f;

        private Vector3 _menuSidebarInitialPosition;
        private Vector3[] _menuButtonsInitialPosition;
        private Vector3 _slotsPanelInitialPosition;
        private bool _isShowingSlotsSidebar = false;
        private Sequence _saveSlotsActivateSequence;

        private async void Start()
        {
            InputManager.Source.OnCancelButtonPressed += CloseLoadGamePanel;

            await UniTask.NextFrame();

            SetInitialPositions();
            EventSystem.current.SetSelectedGameObject(_mainMenuButtons[0].gameObject);
        }

        private void OnDestroy()
        {
            InputManager.Source.OnCancelButtonPressed -= CloseLoadGamePanel;
        }

        private void CloseLoadGamePanel()
        {
            if (_isShowingSlotsSidebar)
            {
                ReturnToMainMenuFromSaveSlots().Forget();
            }
        }

        private void SetInitialPositions()
        {
            _menuSidebarInitialPosition = _mainMenuSidebar.localPosition;
            _menuButtonsInitialPosition = new Vector3[_mainMenuButtons.Length];
            for (int i = 0; i < _mainMenuButtons.Length; i++)
            {
                _menuButtonsInitialPosition[i] = _mainMenuButtons[i].transform.localPosition;
            }

            _slotsPanelInitialPosition = _saveSlotsSidebar.localPosition;
        }

        public async UniTask ActivateLoadGamePanel()
        {
            EventSystem.current.sendNavigationEvents = false;
            _isShowingSlotsSidebar = true;
            
            HideMainMenuSidebar().Forget();

            _saveSlotsSidebar.gameObject.SetActive(true);
            _slotInfoPanel.gameObject.SetActive(true);
            
            _slotInfoPanel.anchoredPosition = _slotInfoPanelFinalPosition + new Vector2(0, _slotInfoPanelOffsetPosition);

            _saveSlotsActivateSequence?.Kill();

            _saveSlotsActivateSequence = DOTween.Sequence()
                .Join(_saveSlotsSidebar.DOLocalMoveX(_saveSlotsSidebar.localPosition.x - _sidebarDisplacement, _animationSpeed).SetEase(Ease.InOutCubic))
                .Append(_slotInfoPanel.DOAnchorPos(_slotInfoPanelFinalPosition + new Vector2(0, -15f), _startTime * 0.6f).SetEase(Ease.OutCubic))
                .Append(_slotInfoPanel.DOAnchorPos(_slotInfoPanelFinalPosition, _startTime * 0.4f).SetEase(Ease.OutBack));

            await _saveSlotsActivateSequence.AsyncWaitForCompletion();

            EventSystem.current.sendNavigationEvents = true;
            EventSystem.current.SetSelectedGameObject(_saveSlotsButtons[0].gameObject);
        }

        private async UniTask HideMainMenuSidebar()
        {
            _mainMenuSidebarLayoutGroup.enabled = false;

            for (int i = 0; i < _mainMenuButtons.Length; i++)
            {
                _mainMenuButtons[i].interactable = false;
                _mainMenuButtons[i].transform.DOLocalMoveX(_menuButtonsInitialPosition[i].x - _buttonsDisplacement, _animationSpeed).SetEase(Ease.InOutCubic);
            }

            await _mainMenuSidebar.DOLocalMoveX(_mainMenuSidebar.localPosition.x - _sidebarDisplacement, _animationSpeed).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();

            for (int i = 0; i < _mainMenuButtons.Length; i++)
            {
                _mainMenuButtons[i].gameObject.SetActive(false);
            }
        }

        public async UniTask ReturnToMainMenuFromSaveSlots()
        {
            EventSystem.current.sendNavigationEvents = false;

            await _saveSlotsSidebar.DOLocalMove(_slotsPanelInitialPosition, _animationSpeed).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
            _saveSlotsSidebar.gameObject.SetActive(false);

            await _slotInfoPanel.DOAnchorPos(_slotInfoPanelFinalPosition + new Vector2(0, -_slotInfoPanelExitDisplacement), 0.15f).SetEase(Ease.InSine).AsyncWaitForCompletion();
            await _slotInfoPanel.DOAnchorPos(_slotInfoPanelFinalPosition + new Vector2(0, _slotInfoPanelOffsetPosition), _slotInfoPanelExitDuration).SetEase(Ease.InCubic).AsyncWaitForCompletion();
            _slotInfoPanel.gameObject.SetActive(false);

            await UniTask.Delay(250);

            await _mainMenuSidebar.DOLocalMove(_menuSidebarInitialPosition, _animationSpeed).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();

            for (int i = 0; i < _mainMenuButtons.Length; i++)
            {
                _mainMenuButtons[i].gameObject.SetActive(true);
                _mainMenuButtons[i].transform.localPosition = _menuButtonsInitialPosition[i] + new Vector3(-_buttonsDisplacement, 0, 0);
                _mainMenuButtons[i].transform.DOLocalMoveX(_menuButtonsInitialPosition[i].x, _animationSpeed).SetEase(Ease.OutCubic);
            }

            await UniTask.Delay((int)(_animationSpeed * 1000));

            for (int i = 0; i < _mainMenuButtons.Length; i++)
            {
                _mainMenuButtons[i].interactable = true;
            }

            _mainMenuSidebarLayoutGroup.enabled = true;
            _isShowingSlotsSidebar = false;

            EventSystem.current.sendNavigationEvents = true;
            EventSystem.current.SetSelectedGameObject(_mainMenuButtons[0].gameObject);
        }
    }
}
