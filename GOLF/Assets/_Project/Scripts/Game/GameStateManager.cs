using System;
using UnityEngine;

namespace Golf
{
    public class GameStateManager : Singleton<IGameStateSource>, IGameStateSource
    {
        public event Action<GameState> OnGameStateChanged;
        public GameState CurrentGameState { get; private set; }

        private void Start()
        {
            InputManager.Source.OnPause += SetPauseState;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnPause -= SetPauseState;
        }

        private void SetPauseState()
        {
            switch (CurrentGameState)
            {
                case GameState.OnPlay:
                    ChangeState(GameState.OnPause);
                    break;
                case GameState.OnPause:
                    ChangeState(GameState.OnPlay);
                    break;
            }
        }

        private void ChangeState(GameState state)
        {
            if (CurrentGameState == state) return; 
            
            CurrentGameState = state;
            OnGameStateChanged?.Invoke(CurrentGameState);
        }
    }
    
    public enum GameState
    { 
        OnPlay,
        OnPause,
    }
}
