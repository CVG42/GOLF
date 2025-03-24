using System;
using UnityEngine;

namespace Golf
{
    public interface IInputSource
    {
        event Action OnConfirmButtonPressed;
        event Action<ACTION_STATE> OnActionChange;
        ACTION_STATE CurrentAction { get; }

        void ChangeAction(ACTION_STATE action);
    }
}
