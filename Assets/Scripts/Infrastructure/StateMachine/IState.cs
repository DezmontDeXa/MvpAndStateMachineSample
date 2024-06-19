namespace Infrastructure
{
	public interface IState
	{
		void Enter();
		void Exit();
		void Back();
		void Restore();
	}
}
