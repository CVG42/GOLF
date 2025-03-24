using UnityEngine;

namespace Golf
{
    public class DirectionHandler : ActionHandler
    {
        public DirectionHandler(BallController _ballController) : base(_ballController) { }

        public override void DoAction()
        {
            if (InputManager.Source.CurrentAction == ACTION_STATE.DIRECTION)
            {
                float input = Input.GetAxis("Horizontal");
                ballController.AdjustThrowDirection(input);
                ballController.UpdateDirectionIndicator();

                if (Input.GetButtonDown("Jump"))
                {
                    ballController.ChangeToForceSelection();
                }
            }
            else if (hasNextHandler)
            {
                nextHandler.DoAction();
            }
        }
    }
}
