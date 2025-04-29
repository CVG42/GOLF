using System;

namespace Golf
{
    public interface IGameStateSource
    {
        event Action<GameState> OnGameStateChanged;
        GameState CurrentGameState { get; }
        void ChangeState(GameState state);
    }
}
