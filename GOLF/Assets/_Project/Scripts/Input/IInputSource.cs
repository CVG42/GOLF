using System;
using UnityEngine;

namespace Golf
{
    public interface IInputSource
    {
        void Enable();
        void Disable();

        event Action OnPreviousButtonPresssed;
        event Action OnNextButtonPresssed;

        event Action OnDeleteButtonPressed;
        event Action OnCancelButtonPressed;

        event Action OnPowerUpActivated;

        event Action OnConfirmButtonPressed;
        event Action<ActionState> OnActionChange;
        event Action<Vector2> OnMoveCamera;
        event Action<float> OnDirectionChange;
        event Action<float> OnForceChange;
        event Action<float, float> OnLaunch;
        event Action OnPause;

        bool IsLocking { get; set; }
        ActionState CurrentActionState { get; }
        void ChangeAction(ActionState newAction);

        event Action<ControllerType> OnControllerTypeChange;
        ControllerType CurrentController { get; }
    }
}
