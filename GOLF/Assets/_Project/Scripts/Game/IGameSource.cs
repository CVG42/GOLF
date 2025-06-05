using System;
using UnityEngine;

namespace Golf
{
    public interface IGameSource
    {
        event Action<int> OnHitsChanged;
        event Action OnLose;
        event Action OnBallRespawn;
        int CurrentHitsLeft { get; }

        void ReduceHitsLeft();
        void TriggerLoseCondition();
        void ResetHitsLeft();
        void RespawnLastPosition();
        void RestoreHitsGameOver();
    }
}
