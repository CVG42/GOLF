using System;

namespace Golf
{
    public interface IPowerSource
    {
        event Action<PowerUpData> OnPowerUpSelected;
        void ApplyPowerUpData(PowerUpData powerUpData);
    }
}
