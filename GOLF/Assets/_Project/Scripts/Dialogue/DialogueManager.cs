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
        [SerializeField] private float _typingSpeed = 0.2f;
        [SerializeField] private GameObject _dialoguePrefab;
        [SerializeField] private Action _onDialogueEnd;

        private bool isTyping = false;
        private bool skipTyping = false;
        private string currentSentence = "";
        private readonly Queue<DialogueLine> _lines = new Queue<DialogueLine>();
             
        public void StartDialogue(Dialogue dialogue, Action onDialogueEnd)
        {
            InputManager.Source.Disable();
            _onDialogueEnd = onDialogueEnd;
            _dialoguePrefab.gameObject.SetActive(true);
            _lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.DialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (isTyping)
            {
                skipTyping = true;
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
            isTyping = true;
            skipTyping = false;
            currentSentence = dialogueline.Line;
            
            AudioManager.Source.PlayOneShot(dialogueline.Character.AudioName);

            _dialogueArea.text = "";
            foreach (char letter in dialogueline.Line.ToCharArray())
            {
                if (skipTyping)
                {
                    _dialogueArea.text = dialogueline.Line;
                    break;
                }

                _dialogueArea.text += letter;
                AudioManager.Source.TypingSFX();
                await UniTask.Delay(TimeSpan.FromSeconds(_typingSpeed), DelayType.DeltaTime);
            }
            isTyping = false;
        }

        private void EndDialogue()
        {
            _dialoguePrefab.gameObject.SetActive(false);
            _onDialogueEnd?.Invoke();
            InputManager.Source.Enable();
        }
    }
}
