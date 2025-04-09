using System;
using UnityEngine;

namespace Golf
{
    public interface IInputSource
    {
        void Enable();
        void Disable();
        
        event Action OnConfirmButtonPressed;
        event Action<ActionState> OnActionChange;
        event Action<Vector2> OnMoveCamera;
        event Action<float> OnDirectionChange;
        event Action<float> OnForceChange;
        event Action<float, float> OnLaunch;

        bool IsLocking { get; set; }
        ActionState CurrentActionState { get; }
        void ChangeAction(ActionState newAction);
    }
}
