using UnityEngine;

namespace Golf
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private void TriggerDialogue()
        {
            GameStateManager.Source.ChangeState(GameState.OnDialogue);
            DialogueManager.Source.StartDialogue(_dialogue, null, true);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                var _rbCollider = collider.GetComponent<Rigidbody2D>();
                TriggerDialogue();
                _rbCollider.angularVelocity = 0f;
                _rbCollider.velocity = Vector3.zero;

                gameObject.SetActive(false);
            }
        }
    }
}