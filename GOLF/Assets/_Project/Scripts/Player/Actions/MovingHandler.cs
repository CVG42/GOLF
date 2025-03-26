namespace Golf
{
    public class MovingHandler : ActionHandler
    {
        public override ActionState ActionState => ActionState.Moving;
        
        public override void DoAction()
        {
            // Ball flies WEEEEE!!!! then it falls down BOOOOO!!!!
        }
    }
}
