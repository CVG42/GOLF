using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

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
        
        private bool _isTyping = false;
        private bool _skipTyping = false;
        private string _currentSentence = "";
        private readonly Queue<DialogueLine> _lines = new Queue<DialogueLine>();

        private void Start()
        {
            InputManager.Source.OnConfirmButtonPressed += NextDialogue;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnConfirmButtonPressed -= NextDialogue;
        }

        public void StartDialogue(Dialogue dialogue, Action onDialogueEnd)
        {
            InputManager.Source.Disable();
            _onDialogueEnd = onDialogueEnd;
            _dialogueFormat.enabled = true;
            _lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.DialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (_isTyping)
            {
                _skipTyping = true;
                return;
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
            _skipTyping = false;
            _currentSentence = dialogueline.Line;

            _dialogueArea.text = "";
            foreach (char letter in dialogueline.Line.ToCharArray())
            {
                if (_skipTyping)
                {
                    _dialogueArea.text = dialogueline.Line;
                    break;
                }

                _dialogueArea.text += letter;
                AudioManager.Source.TypingSFX();
                await UniTask.Delay(TimeSpan.FromSeconds(_typingSpeed), DelayType.DeltaTime);
            }
            _isTyping = false;
        }

        private void EndDialogue()
        {
            _dialogueFormat.enabled = false;
            _onDialogueEnd?.Invoke();
            InputManager.Source.Enable();
        }

        private void NextDialogue()
        {
            _dialogueButton.onClick.Invoke();
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
