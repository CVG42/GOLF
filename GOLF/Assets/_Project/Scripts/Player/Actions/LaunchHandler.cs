using System;

namespace Golf
{
    public class LaunchHandler : ActionHandler
    {
        public Action<float, float> OnLaunch;

        private readonly Func<float> _directionGetter;
        private readonly Func<float> _forceGetter;
        
        public override ActionState ActionState => ActionState.Launch;

        public LaunchHandler(Func<float> directionGetter, Func<float> forceGetter)
        {
            _directionGetter = directionGetter;
            _forceGetter = forceGetter;
        }
        
        public override void DoAction()
        {
            if (InputManager.Source.CurrentActionState != ActionState.Launch) return;

            OnLaunch?.Invoke(_directionGetter(), _forceGetter());
            
            InputManager.Source.ChangeAction(ActionState.Moving);
        }
    }
}
