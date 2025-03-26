using System;

namespace Golf
{
    public interface IInputSource
    {
        event Action OnConfirmButtonPressed;
        event Action<ActionState> OnActionChange;
        event Action<float> OnDirectionChange;
        event Action<float> OnForceChange;
        event Action<float, float> OnLaunch;
        ActionState CurrentAction { get; }
        void ChangeAction(ActionState newAction);
    }
}
