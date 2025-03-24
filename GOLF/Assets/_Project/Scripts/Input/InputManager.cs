using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        public event Action OnConfirmButtonPressed;
        public event Action<ACTION_STATE> OnActionChange;
        public ACTION_STATE CurrentAction => _actionState;

        private ACTION_STATE _actionState;
        private BallController _ballController;
        private ActionHandler _actionHandler;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _ballController = FindObjectOfType<BallController>();

            _actionHandler = new DirectionHandler(_ballController)
                .Chain(new ForceHandler(_ballController))
                .Chain(new LaunchHandler(_ballController));
        }

        private void Update()
        {
            _actionHandler.DoAction();
            CheckOnConfirmButtonPressed();
        }
        
        public void ChangeAction(ACTION_STATE _newAction)
        {
            if (CurrentAction == _newAction) return;
            
            _actionState = _newAction;
            OnActionChange?.Invoke(_actionState);
        }

        private void CheckOnConfirmButtonPressed()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnConfirmButtonPressed?.Invoke();
            }
        }
    }

    public enum ACTION_STATE
    {
        DIRECTION,
        FORCE,
        LAUNCH
    }
}
