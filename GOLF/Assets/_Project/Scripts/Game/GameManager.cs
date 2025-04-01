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

        private bool IsTutorialLevel()
        {
            return SceneManager.GetActiveScene().name != _tutorialLevelScene;
        }

        public void ReduceHitsLeft()
        {
            if (!IsTutorialLevel()) return;

            _strokesNumber = Mathf.Max(0, _strokesNumber - 1);
            OnHitsChanged?.Invoke(_strokesNumber);
        }

        public void ResetHitsLeft()
        {
            _strokesNumber = 5;
        }

        public void TriggerLoseCondition()
        {
            if (!IsTutorialLevel()) return;

            OnLose?.Invoke();
        }
    }
}