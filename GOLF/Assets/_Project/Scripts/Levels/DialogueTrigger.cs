using UnityEngine;

namespace Golf
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        public void TriggerDialogue()
        {
            DialogueManager.Source.StartDialogue(_dialogue);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }
    }
}
