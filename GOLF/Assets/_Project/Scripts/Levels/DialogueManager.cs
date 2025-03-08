using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;

        public Image characterImage;

        public TextMeshProUGUI characterName;
        public TextMeshProUGUI dialogueArea;

        private Queue<DialogueLine> lines = new Queue<DialogueLine>();

        public bool isDialogueActive = false;
        public float typingSpeed = 0.2f;
        //public Animator animator;
        [SerializeField] GameObject dialoguePrefab;

        void Start()
        {

            if (instance == null)
            {
                instance = this;
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            isDialogueActive = true;
            //animator.Play("show");
            dialoguePrefab.gameObject.SetActive(true);
            lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {


            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }

            DialogueLine currentline = lines.Dequeue();


            characterImage.sprite = currentline.character.icon;
            characterName.text = currentline.character.name;

            StopAllCoroutines();

            StartCoroutine(TypeSentence(currentline));


        }
        IEnumerator TypeSentence(DialogueLine dialogueline)
        {
            dialogueArea.text = "";
            foreach (char letter in dialogueline.line.ToCharArray())
            {
                dialogueArea.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        void EndDialogue()
        {
            isDialogueActive = false;
            //animator.Play("hide");
            dialoguePrefab.gameObject.SetActive(false);
        }
    }
}
