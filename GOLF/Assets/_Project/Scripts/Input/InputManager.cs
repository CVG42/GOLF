using System;
using UnityEngine;

namespace Golf
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    { 
        public event Action OnConfirmButtonPressed;
        public event Action<ActionState> OnActionChange;
        public event Action<Vector2> OnMoveCamera;
        public event Action OnPreviousButtonPresssed;
        public event Action OnNextButtonPresssed;
        public event Action OnPause;

        public bool IsLocking { get; set; } = false;
        public ActionState CurrentActionState => _currentAction.ActionState;

        public event Action<float> OnDirectionChange
        {
            add => _directionHandler.OnDirectionChange += value;
            remove => _directionHandler.OnDirectionChange -= value;
        }

        public event Action<float> OnForceChange
        {
            add => _forceHandler.OnForceChange += value;
            remove => _forceHandler.OnForceChange -= value;
        }

        public event Action<float, float> OnLaunch
        {
            add => _launchHandler.OnLaunch += value;
            remove => _launchHandler.OnLaunch -= value;
        }

        private bool _isEnabled = true;
        private ActionHandler _currentAction;
        
        private DirectionHandler _directionHandler;
        private ForceHandler _forceHandler;
        private LaunchHandler _launchHandler;
        private MovingHandler _movingHandler;

        protected override void Awake()
        {
            base.Awake();
            
            _directionHandler = new DirectionHandler();
            _forceHandler = new ForceHandler();
            _launchHandler = new LaunchHandler(_directionHandler.Angle, _forceHandler.Force);
            _movingHandler = new MovingHandler();

            _currentAction = _directionHandler;
        }

        private void Start()
        {
            GameStateManager.Source.OnGameStateChanged += OnGameStatedChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Source.OnGameStateChanged -= OnGameStatedChanged;
        }

        private void Update()
        {
            PauseButton();
            CheckUIButtonInput();

            if (!_isEnabled) return;
            if (GameStateManager.Source.CurrentGameState != GameState.OnPlay) return;

            CheckGameButtonInput();
        }

        public void ChangeAction(ActionState newAction)
        {
            if (!_isEnabled) return;
            if (GameStateManager.Source.CurrentGameState != GameState.OnPlay) return;
            if (CurrentActionState == newAction) return;

            _currentAction = newAction switch
            {
                ActionState.Direction => _directionHandler,
                ActionState.Force => _forceHandler,
                ActionState.Launch => _launchHandler,
                ActionState.Moving => _movingHandler,
                _ => _currentAction,
            };
            
            OnActionChange?.Invoke(CurrentActionState);
        }

        private void CheckUIButtonInput()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnConfirmButtonPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Comma))
            {
                OnPreviousButtonPresssed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                OnNextButtonPresssed?.Invoke();
            }
        }

        private void CheckGameButtonInput()
        {
            if (IsLocking == false)
            {
                var horizontalInput = Input.GetAxis("Horizontal-Camera");
                var verticalInput = Input.GetAxis("Vertical-Camera");

                if (horizontalInput != 0 || verticalInput != 0)
                {
                    OnMoveCamera?.Invoke(new Vector2(horizontalInput, verticalInput));
                }
            }

            _currentAction.DoAction();
        }

        private void PauseButton()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause?.Invoke();
            }
        }

        private void OnGameStatedChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnPlay:
                    Enable();
                    break;
                case GameState.OnPause:
                    Disable();
                    break;
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
