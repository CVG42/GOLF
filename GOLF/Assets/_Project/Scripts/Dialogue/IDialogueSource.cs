using System;
using Golf.Dialogues;

namespace Golf
{
    public interface IDialogueSource
    {
        void StartDialogue(Dialogue dialogue, Action onDialogueEnd = null);
    }
}
