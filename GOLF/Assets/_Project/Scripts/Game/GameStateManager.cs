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

        public Action<GameState> OnGameStateChanged;
        public GameState CurrentGameState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            OnGameStateChanged += ChangeState;
        }

        private void ChangeState(GameState state)
        {
            CurrentGameState = state;
        }

        private void OnDestroy()
        {
            OnGameStateChanged -= ChangeState;
        }
    }
}
