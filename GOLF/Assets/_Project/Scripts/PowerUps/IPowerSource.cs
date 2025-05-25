using System;

namespace Golf
{
    public interface IPowerSource
    {
        event Action<PowerUpData> OnPowerUpSelected;
        PowerUpData CurrentPowerUpData { get; }
        void ApplyPowerUpData(PowerUpData powerUpData);
        bool IsEffectActivated { get; set; }
        bool IsOnCooldown { get; set; } 
        bool TryActivatePowerUp();
        void StrokesCooldown();
    }
}
