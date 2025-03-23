using UnityEngine;

namespace Golf
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private void TriggerDialogue(Collider2D player)
        {
            InputManager inputManager = FindObjectOfType<InputManager>();
            if (inputManager != null)
            {
                inputManager.enabled = false;
            }

            DialogueManager.Source.StartDialogue(_dialogue, () =>
            {
                if (inputManager != null)
                {
                    inputManager.enabled = true;
                }
            });
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                TriggerDialogue(collision);
            }
        }
    }
}