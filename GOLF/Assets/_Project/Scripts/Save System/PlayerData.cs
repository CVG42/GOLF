using UnityEngine;

namespace Golf
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int score;
        private void Start()
        {
            GameData data = SaveSystem.Instance.Load();
            score = data._playerScore;
            transform.position = new Vector3(data._playerPosition.x, data._playerPosition.y, transform.position.z);
        }

        private void SavePlayerData()
        {
            GameData data = new GameData
            {
                _playerScore = score, _playerPosition = (Vector2)transform.position
            };
            SaveSystem.Instance.Save(data);
        }
        private void OnGameQuit()
        {
            SavePlayerData();
        }
    }
}
