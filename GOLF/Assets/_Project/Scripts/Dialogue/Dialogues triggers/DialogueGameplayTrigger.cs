using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Golf
{
    public class DialogueGameplayTrigger : MonoBehaviour
    {
        public enum DialogueType
        {
            Character,
            Camera
        }

        public static List<DialogueGameplayTrigger> _dialogueGameplayTriggers = new();
        public DialogueType type;

        [SerializeField] private Dialogue _dialogue;

        private void Awake()
        {
            _dialogueGameplayTriggers.Add(this);
        }

        private void OnDestroy()
        {
            _dialogueGameplayTriggers.Remove(this);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (type)
            {
                case DialogueType.Character:
                    if (collider.CompareTag("Player"))
                    {
                        DeactivateDialogue().Forget();
                    }
                    break;
                case DialogueType.Camera:
                    if (collider.CompareTag("MainCamera"))
                    {
                        DeactivateDialogue().Forget();
                    }
                    break;
            }
        }

        private async UniTask DeactivateDialogue()
        {
            foreach (var trigger in _dialogueGameplayTriggers)
            {
                trigger.gameObject.SetActive(false);
            }
            _dialogueGameplayTriggers.Remove(this);
            await UniTask.Delay(1000);
            DialogueManager.Source.StartGameplayDialogue(_dialogue, OnDialogueFinished);
        }

        private void OnDialogueFinished()
        {
            foreach (var trigger in _dialogueGameplayTriggers)
            {
                if (trigger != this)
                {
                    trigger.gameObject.SetActive(true);
                }
            }
        }
    }
}
