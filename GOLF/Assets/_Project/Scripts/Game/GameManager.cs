using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class GameManager : Singleton<IGameSource>, IGameSource
    {
        [SerializeField] private float _strokesNumber;

        private readonly string _tutorialLevelScene = "Tutorial";

        public float CurrentHitsLeft { get => _strokesNumber; set => _strokesNumber = Mathf.Max(0, value); }

        public bool IsTutorialLevel()
        {
            return SceneManager.GetActiveScene().name != _tutorialLevelScene;
        }
    }
}