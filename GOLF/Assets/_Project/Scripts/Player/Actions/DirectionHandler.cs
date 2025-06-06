using System;
using UnityEngine;

namespace Golf
{
    public class DirectionHandler : ActionHandler
    {
        private const float DIRECTION_CHANGE_SPEED = 90f;
        private const float INITIAL_ANGLE = 45f;
        
        public Action<float> OnDirectionChange;

        private float _angle = INITIAL_ANGLE;

        public override ActionState ActionState => ActionState.Direction;
        public float Angle() => _angle;

        public override void DoAction()
        {
            AdjustAngleBasedOnInput(Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
            {
                InputManager.Source.ChangeAction(ActionState.Force);
            }
        }

        private void AdjustAngleBasedOnInput(float input)
        {
            _angle -= input * DIRECTION_CHANGE_SPEED * Time.deltaTime;
            float maxAngle = 180f;

            if (PowerUpSystem.Source?.IsEffectActivated == true)
            {
                maxAngle = 360f;
            }

            _angle = Mathf.Clamp(_angle, 0f, maxAngle);
            OnDirectionChange?.Invoke(_angle);
        }
    }
}
