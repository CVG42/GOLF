using UnityEngine;

namespace Golf
{
    public class DialogueGameplayTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private void TriggerDialogue()
        {
            DialogueManager.Source.IsCinematic = false;
            DialogueManager.Source.StartDialogue(_dialogue);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                TriggerDialogue();
                gameObject.SetActive(false);
            }
        }
    }
}
