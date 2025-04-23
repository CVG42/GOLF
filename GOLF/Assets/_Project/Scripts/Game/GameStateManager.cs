using System;
using UnityEngine;

namespace Golf
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        [Serializable]
        public enum GameState
        { 
            OnPlay,
            OnPause,
        }

        public event Action<GameState> OnGameStateChanged;
        public GameState CurrentGameState { get; private set; }

        public void ChangeState(GameState state)
        {
            if (CurrentGameState == state) return; 
            CurrentGameState = state;
            OnGameStateChanged?.Invoke(CurrentGameState);
        }

        private void OnDestroy()
        {
            OnGameStateChanged = null;
        }
    }
}
