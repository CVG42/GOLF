using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Golf
{
    public class GameManager : Singleton<IGameSource>, IGameSource
    {
        private const int INITIAL_STROKES = 50;

        [SerializeField] private int _strokesNumber;

        public event Action<int> OnHitsChanged;
        public event Action OnLose;
        public event Action OnBallRespawn;
        public int CurrentHitsLeft => _strokesNumber;

        public int InitialStrokes => INITIAL_STROKES;

        private readonly HashSet<string> _excludedScenes = new HashSet<string> { "Tutorial", "LevelSelector", "FireballCave" };

        private void Start()
        {
            _strokesNumber = SaveSystem.Source.GetStrokesNumber();
        }

        private bool IsGameLevel()
        {
            return !_excludedScenes.Contains(SceneManager.GetActiveScene().name);
        }

        public void ReduceHitsLeft()
        {
            if (!IsGameLevel()) return;

            _strokesNumber = Mathf.Max(0, _strokesNumber - 1);
            OnHitsChanged?.Invoke(_strokesNumber);
        }

        public void ResetHitsLeft()
        {
            _strokesNumber = InitialStrokes;
        }

        public void TriggerLoseCondition()
        {
            if (!IsGameLevel()) return;

            OnLose?.Invoke();
        }

        public void RespawnLastPosition()
        {
            OnBallRespawn?.Invoke();
        }

        public void RestoreHitsGameOver()
        {
            _strokesNumber = SaveSystem.Source.GetStrokesNumber();
        }
    }
}