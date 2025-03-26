using System;
using UnityEngine;

namespace Golf
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    { 
        public event Action OnConfirmButtonPressed;
        public event Action<ActionState> OnActionChange;

        public ActionState CurrentAction => _actionState;

        public event Action<float> OnDirectionChange
        {
            add { _directionHandler.OnDirectionChange += value; }
            remove { _directionHandler.OnDirectionChange -= value; }
        }

        public event Action<float> OnForceChange
        {
            add { _forceHandler.OnForceChange += value; }
            remove { _forceHandler.OnForceChange -= value; }
        }

        public event Action<float, float> OnLaunch
        {
            add { _launchHandler.OnLaunch += value; }
            remove { _launchHandler.OnLaunch -= value; }
        }

        private ActionState _actionState;
        private ActionHandler _actionHandler;
        private bool _isEnabled = true;

        private DirectionHandler _directionHandler;
        private ForceHandler _forceHandler;
        private LaunchHandler _launchHandler;

        protected override void Awake()
        {
            base.Awake();
            _directionHandler = new DirectionHandler();
            _forceHandler = new ForceHandler();
            _launchHandler = new LaunchHandler(_directionHandler.Angle, _forceHandler.Force);

            _directionHandler
                .Chain(_forceHandler)
                .Chain(_launchHandler);

            _actionHandler = _directionHandler;
        }

        private void Start()
        {
            ChangeAction(ActionState.Direction);
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
        Launch,
        Moving
    }
}
