using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;

        public void TriggerDialogue()
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                TriggerDialogue();
            }
        }
    }
}
