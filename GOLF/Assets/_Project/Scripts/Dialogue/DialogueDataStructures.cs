using System.Collections.Generic;
using UnityEngine;
using System;

namespace Golf
{
    [Serializable]
    public class DialogueCharacter
    {
        public string Name;
        public Sprite Icon;
    }

    [Serializable]
    public class DialogueLine
    {
        public DialogueCharacter Character;
        [TextArea(3, 10)]
        public string Line;
    }

    [Serializable]
    public class Dialogue
    {
        public List<DialogueLine> DialogueLines = new List<DialogueLine>();
    }
}
