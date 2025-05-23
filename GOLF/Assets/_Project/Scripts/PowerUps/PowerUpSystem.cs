using System;
using UnityEngine;

namespace Golf
{
    public class PowerUpSystem : Singleton<IPowerSource>, IPowerSource
    {
        private const int STROKES_REQUIRED = 3;

        public event Action<PowerUpData> OnPowerUpSelected;
        public PowerUpData CurrentPowerUpData => _currentPowerUp;

        public bool IsEffectActivated { get; set; } = false;

        private PowerUpData _currentPowerUp;
        private bool _isOnCooldown = false;
        private int _strikesSinceLastActivated = 0;

        public void ApplyPowerUpData(PowerUpData powerUpData)
        {
            IsEffectActivated = false;
            _currentPowerUp = powerUpData;
            OnPowerUpSelected?.Invoke(powerUpData);
        }

        public bool TryActivatePowerUp()
        {
            if (_isOnCooldown || _currentPowerUp.HasEffect == false) return false;

            _isOnCooldown = true;
            _strikesSinceLastActivated = 0;
            return true;
        }

        public void StrokesCooldown()
        {
            if(!_isOnCooldown) return;

            _strikesSinceLastActivated++;
            if (_strikesSinceLastActivated >= STROKES_REQUIRED)
            {
                _isOnCooldown = false;
                _strikesSinceLastActivated = 0;
            }
        }
    }
}
