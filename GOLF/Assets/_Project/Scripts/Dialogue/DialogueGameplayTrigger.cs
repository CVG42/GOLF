using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class DialogueGameplayTrigger : MonoBehaviour
    {
        public static List<DialogueGameplayTrigger> _dialogueGameplayTriggers = new();

        private Collider2D _triggerCollider;
        [SerializeField] private Dialogue _dialogue;

        private void Awake()
        {
            _dialogueGameplayTriggers.Add(this);
            _triggerCollider = GetComponent<Collider2D>();
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
                    trigger._triggerCollider.enabled = false;
                }

                DialogueManager.Source.StartGameplayDialogue(_dialogue, OnDialogueFinished);
            }
        }

        private void OnDialogueFinished()
        {
            foreach (var trigger in _dialogueGameplayTriggers)
            {
                if (trigger != this)
                {
                    trigger._triggerCollider.enabled = true;
                }
            }
        }
    }
}
