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
        [SerializeField] private float _typingSpeed = 0.2f;
        [SerializeField] private Canvas _dialogueFormat;
        [SerializeField] private Action _onDialogueEnd;
        [SerializeField] private RectTransform _dialogueRectTransform;
        
        private bool _isTyping = false;
        private bool _skipTyping = false;
        private string _currentSentence = "";
        private readonly Queue<DialogueLine> _lines = new Queue<DialogueLine>();

        public bool IsCinematic { get; set; } = false;

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= NextDialogue;
        }

        public void StartDialogue(Dialogue dialogue, Action onDialogueEnd)
        {
            if (IsCinematic) {
                InputManager.Source.OnConfirmButtonPressed += NextDialogue;
                InputManager.Source.Disable();
            }

            _onDialogueEnd = onDialogueEnd;
            EnableDialogue();
            _lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.DialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (IsCinematic)
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

            _characterImage.sprite = currentline.Character.Icon;
            _characterName.text = currentline.Character.Name;

            TypeSentence(currentline);
        }

        private async void TypeSentence(DialogueLine dialogueline)
        {
            _isTyping = true;
            if (IsCinematic)
            {                
                _skipTyping = false;
            }

            _currentSentence = dialogueline.Line;

            _dialogueArea.text = "";
            foreach (char letter in dialogueline.Line.ToCharArray())
            {
                if (IsCinematic)
                {
                    if (_skipTyping)
                    {
                        _dialogueArea.text = dialogueline.Line;
                        break;
                    }
                }

                _dialogueArea.text += letter;
                if (IsCinematic)
                {
                    AudioManager.Source.TypingSFX();
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingSpeed), DelayType.DeltaTime);
                }
                else
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.05), DelayType.DeltaTime);
                }

            }
            if (IsCinematic)
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
            if (IsCinematic)
            {
                _onDialogueEnd?.Invoke();
                InputManager.Source.Enable();
            }
            _ = DisableDialogue();
        }

        private void NextDialogue()
        {
            _dialogueButton.onClick.Invoke();
        }

        private void EnableDialogue()
        {
            if (IsCinematic) {
                _dialogueFormat.enabled = true;
                _dialogueRectTransform.DOAnchorPosY(0, 0.5f, true);
            }
            else
            {
                _dialogueFormat.enabled = true;
                _dialogueRectTransform.DOAnchorPosX(0, 0.5f, true);
            }
        }

        private async UniTask DisableDialogue()
        {
            if (IsCinematic)
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

    [Serializable]
    public class DialogueLine
    {
        public DialogueCharacter Character;
        [TextArea(3, 10)]
        public string Line;
    }

    [Serializable]
    public class DialogueLine
    {
        public DialogueCharacter Character;
        [TextArea(3, 10)]
        public string Line;
    }
}
