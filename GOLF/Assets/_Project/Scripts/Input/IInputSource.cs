using System;
using UnityEngine;

namespace Golf
{
    public interface IInputSource
    {
        float CurrentAngle { get; }
        Action<Vector2> OnLaunchBall { get; set; }
        Vector2 GetCurrentAngleDirection();
    }
}
