using System;
using UnityEngine;

namespace Golf
{
    public class PowerUpSystem : Singleton<IPowerSource>, IPowerSource
    {
        public event Action<PowerUpData> OnPowerUpSelected;

        public void ApplyPowerUpData(PowerUpData powerUpData)
        {
            OnPowerUpSelected?.Invoke(powerUpData);
        }
    }
}
