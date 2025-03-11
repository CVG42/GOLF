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

        [SerializeField] private Image _characterImage;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _dialogueArea;
        private Queue<DialogueLine> _lines = new Queue<DialogueLine>();
        [SerializeField] private bool _isDialogueActive = false;
        [SerializeField] private float _typingSpeed = 0.2f;
        [SerializeField] private GameObject _dialoguePrefab;

        void Start()
        {

            if (instance == null)
            {
                instance = this;
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            _isDialogueActive = true;

            _dialoguePrefab.gameObject.SetActive(true);
            _lines.Clear();

            foreach (DialogueLine dialogueline in dialogue.dialogueLines)
            {
                _lines.Enqueue(dialogueline);
            }

            DisplayNextDialogueLine();
        }

        public void DisplayNextDialogueLine()
        {
            if (_lines.Count == 0)
            {
                EndDialogue();
                return;
            }

            DialogueLine currentline = _lines.Dequeue();

            _characterImage.sprite = currentline.character.icon;
            _characterName.text = currentline.character.name;

            StopAllCoroutines();

            StartCoroutine(TypeSentence(currentline));
        }

        IEnumerator TypeSentence(DialogueLine dialogueline)
        {
            _dialogueArea.text = "";
            foreach (char letter in dialogueline.line.ToCharArray())
            {
                _dialogueArea.text += letter;
                yield return new WaitForSeconds(_typingSpeed);
            }
        }

        private void EndDialogue()
        {
            _isDialogueActive = false;

            _dialoguePrefab.gameObject.SetActive(false);
        }
    }
}
