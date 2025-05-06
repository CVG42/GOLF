using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class PowerUpSelector : MonoBehaviour
    {
        [SerializeField] private List<PowerUpData> _availablePowerUps;
        [SerializeField] private List<RuntimeAnimatorController> _powerUpAnimators;
        [SerializeField] private Image _powerUpIconImage;
        [SerializeField] private Animator _powerUpAnimator;

        private int _currentPowerUpIndex = 0;
        private IInputSource _inputSource;
        private IPowerSource _powerUpSource;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _powerUpSource = PowerUpSystem.Source;
        }

        private void Start()
        {
            _inputSource.OnNextButtonPresssed += HandleNextPowerUp;
            _inputSource.OnPreviousButtonPresssed += HandlePreviousPowerUp;

            UpdateUI();
        }

        private void OnDestroy()
        {
            _inputSource.OnNextButtonPresssed -= HandleNextPowerUp;
            _inputSource.OnPreviousButtonPresssed -= HandlePreviousPowerUp;
        }

        private void HandleNextPowerUp() => SelectPowerUp(1);
        private void HandlePreviousPowerUp() => SelectPowerUp(-1);

        private void SelectPowerUp(int selectionDirection)
        {
            _currentPowerUpIndex += selectionDirection;

            if (_currentPowerUpIndex >= _availablePowerUps.Count)
            {
                _currentPowerUpIndex = 0;
            }

            if (_currentPowerUpIndex < 0)
            {
                _currentPowerUpIndex = _availablePowerUps.Count - 1;
            }

            UpdateUI();

            _powerUpSource.ApplyPowerUpData(_availablePowerUps[_currentPowerUpIndex]);
        }

        private void UpdateUI()
        {
            if (_currentPowerUpIndex >= 0 && _currentPowerUpIndex < _availablePowerUps.Count)
            {
                _powerUpIconImage.sprite = _availablePowerUps[_currentPowerUpIndex].BallSprite;
                PlayPowerUpAnimation(_currentPowerUpIndex);
            }
        }

        private void PlayPowerUpAnimation(int index)
        {
            if (_powerUpAnimators == null || index >= _powerUpAnimators.Count || _powerUpAnimators[index] == null)
                return;

            _powerUpAnimator.runtimeAnimatorController = _powerUpAnimators[index];
        }
    }
}
