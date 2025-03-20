using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class GameManager : Singleton<IGameSource>, IGameSource
    {
        [SerializeField] private int _strokesNumber;

        public event Action<int> OnHitsChanged;
        public event Action OnLose;
        public int CurrentHitsLeft => _strokesNumber;

        private readonly string _tutorialLevelScene = "Tutorial";

        public bool IsTutorialLevel()
        {
            return SceneManager.GetActiveScene().name != _tutorialLevelScene;
        }

        public void ReduceHitsLeft()
        {
            _strokesNumber = Mathf.Max(0, _strokesNumber - 1);
            OnHitsChanged?.Invoke(_strokesNumber);

            if (_strokesNumber == 0)
            {
                OnLose?.Invoke();
            }
        }
    }
}