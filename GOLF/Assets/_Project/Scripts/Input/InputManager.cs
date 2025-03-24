using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        private const float INITIAL_ANGLE = 45f;
        
        public event Action OnConfirmButtonPressed;
        public event Action<ActionState> OnActionChange;
        public event Action<float> OnDirectionChange;
        public event Action<float, float> OnLaunch;
        public ActionState CurrentAction => _actionState;

        private ActionState _actionState;
        private BallController _ballController;
        private ActionHandler _actionHandler;
        private bool _isEnabled = true;
        private float _angle = INITIAL_ANGLE;
        private float _force;

        private void Start()
        {
            _actionHandler = new DirectionHandler(ref _angle, OnDirectionChange)
                .Chain(new ForceHandler(_ballController))
                .Chain(new LaunchHandler(ref _angle, ref _force, OnLaunch));
        }

        private void Update()
        {
            if (!_isEnabled) return;

            _actionHandler.DoAction();
            CheckOnConfirmButtonPressed();
        }
        
        public void ChangeAction(ActionState newAction)
        {
            if (CurrentAction == newAction) return;
            
            _actionState = newAction;
            OnActionChange?.Invoke(_actionState);
        }

        private void CheckOnConfirmButtonPressed()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnConfirmButtonPressed?.Invoke();
            }
        }
        
        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }
    }

    public enum ActionState
    {
        Direction,
        Force,
        Launch
    }
}
