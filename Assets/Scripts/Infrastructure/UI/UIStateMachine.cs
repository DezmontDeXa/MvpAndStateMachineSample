namespace Infrastructure
{
	public class UIStateMachine : StateMachine<StatePresenterBase<AnimatedViewBase>>
	{
		public UIStateMachine(StateFactory stateFactory) : base(stateFactory)
		{
		}
	}
}
