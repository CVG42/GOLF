using System;
using UnityEngine;

namespace Golf
{
    public class ForceHandler : ActionHandler
    {
        private const float INITIAL_FORCE = 0f;
        private const float FORCE_SLIDER_SPEED = 8f;

        public Action<float> OnForceChange;

        private float _forceTimer = 0;
        private float _force = INITIAL_FORCE;

        public override ActionState ActionState => ActionState.Force;
        public float Force() => _force;

        public override void DoAction()
        {
            SetUpForceFromSlider();

            if (Input.GetButtonDown("Jump"))
            {
                InputManager.Source.ChangeAction(ActionState.Launch);
            }
        }

        private void SetUpForceFromSlider()
        {
            _forceTimer += Time.deltaTime;
            _force = Mathf.PingPong(_forceTimer * FORCE_SLIDER_SPEED, 9f) + 1;
            OnForceChange?.Invoke(_force);
        }
    }
}
