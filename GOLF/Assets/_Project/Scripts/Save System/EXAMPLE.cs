using UnityEngine;
namespace Golf
{
    public class EXAMPLE : MonoBehaviour
    {
        [SerializeField] private int score;
        private void Start()
        {
            GameData data = SaveSystem.Load();
            score = data.playerScore;
            transform.position = data.playerPosition;
            Debug.Log("Loading game data: " + data.playerScore + data.playerPosition);
        }

        private void SavePlayerData()
        {
            GameData data = new GameData
            {
                playerScore = score, playerPosition = (Vector2)transform.position
            };
            SaveSystem.Save(data);
        }
        private void OnGameQuit()
        {
            SavePlayerData();
        }
    }
}
