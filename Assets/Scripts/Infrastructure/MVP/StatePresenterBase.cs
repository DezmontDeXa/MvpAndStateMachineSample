using Zenject;

namespace Infrastructure
{
	public abstract class StatePresenterBase<TView> : IState where TView : IView
	{
		private TView _view;
		private UIStateMachine _stateMachine;

		protected TView View => _view;
		protected UIStateMachine UIStateMachine => _stateMachine;

		[Inject]
		private void Constructor(TView view, UIStateMachine stateMachine)
		{
			_view = view;
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			_view.Show();
			OnEnter();
		}

		public void Exit()
		{
			_view.Hide();
			OnExit();
		}

		public void Back()
		{
			_view.Back();
			OnBack();
		}

		public void Restore()
		{
			_view.Restore();
			OnRestore();
		}

		protected virtual void OnEnter() { }
		protected virtual void OnExit() { }
		protected virtual void OnBack() { }
		protected virtual void OnRestore() { }
	}
}
