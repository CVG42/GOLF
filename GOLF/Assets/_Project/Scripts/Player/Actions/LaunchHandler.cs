using System;

namespace Golf
{
    public class LaunchHandler : ActionHandler
    {
        public Action<float, float> OnLaunch;

        private Func<float> _directionGetter;
        private Func<float> _forceGetter;

        public LaunchHandler(Func<float> directionGetter, Func<float> forceGetter)
        {
            _directionGetter = directionGetter;
            _forceGetter = forceGetter;
        }
        
        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction != ActionState.Launch) return;

            OnLaunch?.Invoke(_directionGetter(), _forceGetter());
            InputManager.Source.ChangeAction(ActionState.Moving);

            if (hasNextHandler && InputManager.Source.CurrentAction != ActionState.Moving)
            {
                nextHandler.DoAction();
            }
        }
    }
}
