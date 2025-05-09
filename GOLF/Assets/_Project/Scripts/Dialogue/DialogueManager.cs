using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Golf.Dialogues;

namespace Golf
{
    public class DialogueManager : Singleton<IDialogueSource>, IDialogueSource
    {
        [SerializeField] private Canvas _dialogueCanvas;

        [Header("Cinematic format")]
        [SerializeField] private Image _characterCinematicImage;
        [SerializeField] private TextMeshProUGUI _characterCinematicName;
        [SerializeField] private TextMeshProUGUI _dialogueCinematicArea;
        [SerializeField] private Button _dialogueButton;
        [SerializeField] private float _typingCinematicSpeed = 0.2f;
        [SerializeField] private RectTransform _dialogueCinematicRectTransform;
        [SerializeField] private Action _onDialogueEnd;

        [Header("Gameplay format")]
        [SerializeField] private Image _characterGameplayImage;
        [SerializeField] private TextMeshProUGUI _characterGameplayName;
        [SerializeField] private TextMeshProUGUI _dialogueGameplayArea;
        [SerializeField] private float _typingGameplaySpeed = 0.05f;
        [SerializeField] private RectTransform _dialogueGameplayRectTransform;

        private bool _isTyping = false;
        private bool _skipTyping = false;
        private bool _isCinematic = true;
        private string _currentSentence = "";
        private readonly Queue<DialogueLine> _lines = new Queue<DialogueLine>();

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= NextDialogue;
        }

        public void StartDialogue(Dialogue dialogue, Action onDialogueEnd, bool isCinematic)
        {
            _lines.Clear();
            _isCinematic = isCinematic;
            if (_isCinematic) {
                InputManager.Source.OnConfirmButtonPressed += NextDialogue;
                InputManager.Source.Disable();
            }

            _onDialogueEnd = onDialogueEnd;
            EnableDialogue();

            foreach (DialogueLine dialogueline in dialogue.DialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (_isCinematic)
            {
                if (_isTyping)
                {
                    _skipTyping = true;
                    return;
                }
            }

            if (_lines.Count == 0)
            {
                EndDialogue();
                return;
            }

            DialogueLine currentline = _lines.Dequeue();

            if (_isCinematic)
            {
                _characterCinematicImage.sprite = currentline.Character.Icon;
                _characterCinematicName.text = currentline.Character.Name.Localize();
            }
            else
            {
                _characterGameplayImage.sprite = currentline.Character.Icon;
                _characterGameplayName.text = currentline.Character.Name.Localize();
            }

            TypeSentence(currentline);
        }

        private async void TypeSentence(DialogueLine dialogueline)
        {
            if (_isCinematic)
            {
                _dialogueCinematicArea.text = "";
            }
            else
            {
                _dialogueGameplayArea.text = "";
            }
            _isTyping = true;
            if (_isCinematic)
            {                
                _skipTyping = false;
            }

            _currentSentence = dialogueline.Line.Localize();

            foreach (char letter in _currentSentence)
            {
                if (_isCinematic)
                {
                    if (_skipTyping)
                    {
                        _dialogueCinematicArea.text = _currentSentence;
                        break;
                    }
                }

                if (_isCinematic)
                {
                    _dialogueCinematicArea.text += letter;
                }
                else
                {
                    _dialogueGameplayArea.text += letter;
                }

                if (_isCinematic)
                {
                    AudioManager.Source.TypingSFX();
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingCinematicSpeed), DelayType.DeltaTime);
                }
                else
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingGameplaySpeed), DelayType.DeltaTime);
                }

            }
            if (_isCinematic)
            {
                _isTyping = false;
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                DisplayNextDialogueLine();
            }

        }

        private void EndDialogue()
        {
            if (_isCinematic)
            {
                _onDialogueEnd?.Invoke();
                InputManager.Source.Enable();
            }
            DisableDialogue().Forget();
        }

        private void NextDialogue()
        {
            _dialogueButton.onClick.Invoke();
        }

        private void EnableDialogue()
        {
            _dialogueCanvas.enabled = true;

            if (_isCinematic) 
            {
                _dialogueCinematicRectTransform.DOAnchorPosY(0, 0.5f, true);
            }
            else
            {
                _dialogueGameplayRectTransform.DOAnchorPosX(0, 0.5f, true);
            }
        }

        private async UniTask DisableDialogue()
        {
            if (_isCinematic)
            {
                _dialogueCinematicRectTransform.DOAnchorPosY(-215, 0.5f, true).WaitForCompletion();
                await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.DeltaTime);
                _dialogueCanvas.enabled = false;
            }
            else
            {
                _dialogueGameplayRectTransform.DOAnchorPosX(761, 0.5f, true);
                await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.DeltaTime);
                _dialogueCanvas.enabled = false;
            }
        }
    }

    [Serializable]
    public class DialogueLine
    {
        public DialogueCharacter Character;
        [TextArea(3, 10)]
        public string Line;
    }
}
