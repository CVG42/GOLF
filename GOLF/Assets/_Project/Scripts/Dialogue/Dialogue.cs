﻿using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<DialogueLine> DialogueLines = new List<DialogueLine>();
    }
}
