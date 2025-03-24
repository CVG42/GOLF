using UnityEngine;

namespace Golf
{
    public abstract class ActionHandler
    {
        public ActionHandler nextHandler;
        
        protected bool hasNextHandler => nextHandler != null;

        public abstract void DoAction();

        public ActionHandler Chain(ActionHandler actionHandler)
        {
            ActionHandler handler = this;

            while (handler.nextHandler != null) 
            {
                handler = handler.nextHandler;
            }

            handler.nextHandler = actionHandler;
            return this;
        }
    }
}
