using System;
using UnityEngine;

namespace Golf
{
    public class ForceHandler : ActionHandler
    {
        private const float INITIAL_FORCE = 0f;

        public Action<float> OnForceChange;

        private float _forceTimer = 0;
        private readonly float _forceSliderSpeed = 8f;
        private bool _isButtonReady = false;
        private float _force = INITIAL_FORCE;

        public float Force() => _force;

        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ActionState.Force)
            {
                SetUpForceFromSlider();
                OnForceChange?.Invoke(_force);

                if (!_isButtonReady) 
                {
                    if (Input.GetButtonUp("Jump"))
                    {
                        _isButtonReady = true;
                    }
                }

                if (Input.GetButtonDown("Jump"))
                {
                    InputManager.Source.ChangeAction(ActionState.Launch);
                  
                    _isButtonReady = false;
                }
            }
            else if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }

        private void SetUpForceFromSlider()
        {
            _forceTimer += Time.deltaTime;
            _force = Mathf.PingPong(_forceTimer * _forceSliderSpeed, 9f) + 1;
        }
    }
}
