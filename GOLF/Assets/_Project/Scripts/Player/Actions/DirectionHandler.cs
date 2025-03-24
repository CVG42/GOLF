using System;
using UnityEngine;

namespace Golf
{
    public class DirectionHandler : ActionHandler
    {
        private const float DIRECTION_CHANGE_SPEED = 90f;
        
        private readonly Action<float> _onDirectionChange;
        private float _angle;
        
        public DirectionHandler(ref float angle, Action<float> onDirectionChange)
        {
            _angle = angle;
            _onDirectionChange = onDirectionChange;
        }

        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ActionState.Direction)
            {
                AdjustAngleBasedOnInput(Input.GetAxis("Horizontal"));
                _onDirectionChange?.Invoke(_angle);

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
            _angle = Mathf.Clamp(_angle, 0f, 180f);
        }
    }
}
