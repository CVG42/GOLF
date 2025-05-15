using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class DialogueGameplayTrigger : MonoBehaviour
    {
        public static List<DialogueGameplayTrigger> _dialogueGameplayTriggers = new();

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
            if (collider.CompareTag("Player"))
            {                
                foreach (var trigger in _dialogueGameplayTriggers)
                {
                    trigger.gameObject.SetActive(false);
                }
                _dialogueGameplayTriggers.Remove(this);
                DialogueManager.Source.StartGameplayDialogue(_dialogue, OnDialogueFinished);
            }
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
