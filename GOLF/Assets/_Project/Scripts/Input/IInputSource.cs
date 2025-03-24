using System;
using UnityEngine;

namespace Golf
{
    public interface IInputSource
    {
        event Action OnConfirmButtonPressed { get; set; }
        event Action<ACTION_STATE> OnActionChange { get; set; }
        ACTION_STATE CurrentAction { get; }

        void ChangeAction(ACTION_STATE action);
    }
}
