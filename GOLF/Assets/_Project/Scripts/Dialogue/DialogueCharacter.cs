﻿using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "DialogueCharacter", menuName = "Dialogue System/DialogueCharacter")]
    public class DialogueCharacter : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string AudioName;
    }
}
