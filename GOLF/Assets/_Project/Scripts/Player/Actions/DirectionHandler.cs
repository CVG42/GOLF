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

        public float Angle() => _angle;

        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ActionState.Direction)
            {
                AdjustAngleBasedOnInput(Input.GetAxis("Horizontal"));

                if (Input.GetButtonDown("Jump"))
                {
                    InputManager.Source.ChangeAction(ActionState.Force);
                }
            }
            else if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }

        private void AdjustAngleBasedOnInput(float input)
        {
            _angle -= input * DIRECTION_CHANGE_SPEED * Time.deltaTime;
            OnDirectionChange?.Invoke(_angle);
        }
    }
}
