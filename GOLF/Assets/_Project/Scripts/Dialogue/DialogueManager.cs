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
        [SerializeField] private Image _characterImage;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _dialogueArea;
        [SerializeField] private Button _dialogueButton;
        [SerializeField] private float _typingCinematicSpeed = 0.2f, _typingGameplaySpeed = 0.05f;
        [SerializeField] private Canvas _dialogueFormat;
        [SerializeField] private Action _onDialogueEnd;
        [SerializeField] private RectTransform _dialogueRectTransform;
        
        private bool _isTyping = false;
        private bool _skipTyping = false;
        private string _currentSentence = "";
        private readonly Queue<DialogueLine> _lines = new Queue<DialogueLine>();

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= NextDialogue;
        }

        public void StartDialogue(Dialogue dialogue, Action onDialogueEnd, bool isCinematic)
        {
            if (isCinematic) {
                InputManager.Source.OnConfirmButtonPressed += NextDialogue;
                InputManager.Source.Disable();
            }

            _onDialogueEnd = onDialogueEnd;
            EnableDialogue(isCinematic);
            _lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.DialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine(isCinematic);
        }

        public void DisplayNextDialogueLine(bool isCinematic)
        {
            if (isCinematic)
            {
                if (_isTyping)
                {
                    _skipTyping = true;
                    return;
                }
            }

            if (_lines.Count == 0)
            {
                EndDialogue(isCinematic);
                return;
            }

            DialogueLine currentline = _lines.Dequeue();

            _characterImage.sprite = currentline.Character.Icon;
            _characterName.text = currentline.Character.Name;

            TypeSentence(currentline, isCinematic);
        }

        private async void TypeSentence(DialogueLine dialogueline, bool isCinematic)
        {
            _isTyping = true;
            if (isCinematic)
            {                
                _skipTyping = false;
            }

            _currentSentence = dialogueline.Line;

            _dialogueArea.text = "";
            foreach (char letter in dialogueline.Line.ToCharArray())
            {
                if (isCinematic)
                {
                    if (_skipTyping)
                    {
                        _dialogueArea.text = dialogueline.Line;
                        break;
                    }
                }

                _dialogueArea.text += letter;
                if (isCinematic)
                {
                    AudioManager.Source.TypingSFX();
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingCinematicSpeed), DelayType.DeltaTime);
                }
                else
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingGameplaySpeed), DelayType.DeltaTime);
                }

            }
            if (isCinematic)
            {
                _isTyping = false;
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                DisplayNextDialogueLine(isCinematic);
            }

        }

        private void EndDialogue(bool isCinematic)
        {
            if (isCinematic)
            {
                _onDialogueEnd?.Invoke();
                InputManager.Source.Enable();
            }
            DisableDialogue(isCinematic).Forget();
        }

        private void NextDialogue()
        {
            _dialogueButton.onClick.Invoke();
        }

        private void EnableDialogue(bool isCinematic)
        {
            if (isCinematic) {
                _dialogueFormat.enabled = true;
                _dialogueRectTransform.DOAnchorPosY(0, 0.5f, true);
            }
            else
            {
                _dialogueFormat.enabled = true;
                _dialogueRectTransform.DOAnchorPosX(0, 0.5f, true);
            }
        }

        private async UniTask DisableDialogue(bool isCinematic)
        {
            if (isCinematic)
            {
                _dialogueRectTransform.DOAnchorPosY(-215, 0.5f, true).WaitForCompletion();
                await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.DeltaTime);
                _dialogueFormat.enabled = false;
            }
            else
            {
                _dialogueRectTransform.DOAnchorPosX(761, 0.5f, true);
                await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.DeltaTime);
                _dialogueFormat.enabled = false;
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
