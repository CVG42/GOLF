using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        void Save(GameData data);
        GameData Load();
        float GetVolume();
        void SetVolume(float volume);
    }
}
