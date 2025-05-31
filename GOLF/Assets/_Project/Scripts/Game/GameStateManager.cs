using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (SceneManager.GetActiveScene().name == "MainMenu") return;

            switch (CurrentGameState)
            {
                case GameState.OnPlay:
                    ChangeState(GameState.OnPause);
                    break;
                case GameState.OnPause:
                    ChangeState(GameState.OnPlay);
                    break;
                case GameState.OnDialogue:
                    ChangeState(GameState.OnDialogue); 
                    break;
                case GameState.OnGameOver:
                    ChangeState(GameState.OnGameOver);
                    break;
            }
        }

        public void ChangeState(GameState state)
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
        OnDialogue,
        OnGameOver
    }
}
