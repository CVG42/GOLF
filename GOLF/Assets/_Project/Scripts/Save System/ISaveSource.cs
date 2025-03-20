using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        float GetVolume();
        void SetVolume(float volume);
    }
}
