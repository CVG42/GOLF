using System;

namespace Golf
{
    public class LaunchHandler : ActionHandler
    {
        private Action<float, float> _onLaunch;
        private readonly float _angle;
        private readonly float _force;
        
        public LaunchHandler(ref float angle, ref float force, Action<float, float> onLaunch)
        {
            _angle = angle;
            _force = force;
        }
        
        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ActionState.Launch) return;

            _onLaunch?.Invoke(_angle, _force);
            
            if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }
    }
}
