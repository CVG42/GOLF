using UnityEngine;

namespace Golf
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private void TriggerDialogue()
        {
            DialogueManager.Source.StartDialogue(_dialogue);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }
    }
}