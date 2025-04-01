namespace Golf
{
    public abstract class ActionHandler
    {
        public abstract ActionState ActionState { get; }

        public abstract void DoAction();
    }
}
