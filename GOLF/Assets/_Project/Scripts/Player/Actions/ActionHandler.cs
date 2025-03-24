using UnityEngine;

namespace Golf
{
    public abstract class ActionHandler
    {
        public ActionHandler(BallController _ballController)
        {
            ballController = _ballController;
        }
        public ActionHandler nextHandler;
        
        protected BallController ballController;
        protected bool hasNextHandler => nextHandler != null;

        public abstract void DoAction();

        public ActionHandler Chain(ActionHandler _actionHandler)
        {
            ActionHandler handler = this;

            while (handler.nextHandler != null) 
            {
                handler = handler.nextHandler;
            }

            handler.nextHandler = _actionHandler;
            return this;
        }
    }
}
