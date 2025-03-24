namespace Golf
{
    public class LaunchHandler : ActionHandler
    {
        public LaunchHandler(BallController _ballController) : base(_ballController) { }

        public override void DoAction()
        {
            if(InputManager.Source.CurrentAction == ACTION_STATE.LAUNCH)
            {
                ballController.StopBallCheck();
            }
            else if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }
    }
}
