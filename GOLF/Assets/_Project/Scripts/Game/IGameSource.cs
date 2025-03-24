using System;

namespace Golf
{
    public interface IGameSource
    {
        event Action<int> OnHitsChanged;
        event Action OnLose;
        int CurrentHitsLeft { get; }
        void ReduceHitsLeft();
        void TriggerLoseCondition();
        void ResetHitsLeft();
    }
}
