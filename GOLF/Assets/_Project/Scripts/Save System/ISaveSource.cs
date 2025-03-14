using UnityEngine;

namespace Golf
{
    public interface ISaveSource
    {
        void Save(GameData data);
        GameData Load();
        int GetScore();
        void SetScore(int score);
        Vector2 GetPlayerPosition();
        void SetPlayerPosition(Vector2 position);
        float GetVolume();
        void SetVolume(float volume);
    }
}
