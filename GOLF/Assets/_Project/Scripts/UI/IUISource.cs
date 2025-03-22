using UnityEngine;

namespace Golf
{
    public interface IUISource
    {
        void UpdateHitsLeft(int hitsLeft);
        void ShowLosePanel();
    }
}
