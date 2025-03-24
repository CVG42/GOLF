using UnityEngine;

namespace Golf
{
    public class ForceHandler : ActionHandler
    {
        public ForceHandler(BallController _ballController) : base(_ballController) { }

        private bool _isButtonReady = false;

        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ACTION_STATE.FORCE)
            {
                ballController.SetStrokeForce();

                if (!_isButtonReady) 
                {
                    if (Input.GetButtonUp("Jump"))
                    {
                        _isButtonReady=true;
                    }
                }

                if (Input.GetButtonDown("Jump"))
                {
                    ballController.LaunchBall();
                    _isButtonReady = false;
                }
            }
            else if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }
    }
}
