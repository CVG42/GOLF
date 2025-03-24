using System.Collections.Generic;
using UnityEngine;
using System;

namespace Golf
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<DialogueLine> DialogueLines = new List<DialogueLine>();
    }

    [CreateAssetMenu(fileName = "DialogueCharacter", menuName = "Dialogue System/DialogueCharacter")]
    public class DialogueCharacter : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string AudioName;
    }

    [Serializable]
    public class DialogueLine
    {
        public DialogueCharacter Character;
        [TextArea(3, 10)]
        public string Line;
    }   
}
