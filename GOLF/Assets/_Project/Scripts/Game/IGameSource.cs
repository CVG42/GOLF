using UnityEngine;

namespace Golf
{
    public interface IGameSource
    {
        float CurrentHitsLeft { get; set; }
        bool IsTutorialLevel();
    }
}
