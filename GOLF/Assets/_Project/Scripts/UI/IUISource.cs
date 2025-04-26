using UnityEngine;

namespace Golf
{
    public interface IUISource
    {
        void UpdateHitsLeft(int hitsLeft);
        void ShowLosePanel();
        void HidePanels();
        void ActivatePausePanel();
        void OnGameStateChanged(GameState newState);
    }
}
