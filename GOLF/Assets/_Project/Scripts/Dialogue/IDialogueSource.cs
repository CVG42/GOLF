using System;

namespace Golf
{
    public interface IDialogueSource
    {
        void StartDialogue(Dialogue dialogue, Action onDialogueEnd = null);
    }
}
