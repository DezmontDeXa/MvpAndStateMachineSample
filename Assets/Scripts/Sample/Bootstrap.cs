using Cysharp.Threading.Tasks;
using Infrastructure;
using System;
using Zenject;

namespace Sample
{
	public class Bootstrap : IInitializable
	{
		private readonly UIStateMachine _stateMachine;

		public Bootstrap(UIStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void Initialize()
		{
			WaitInitFinishAndSetFirstPage();
		}

		private async UniTask WaitInitFinishAndSetFirstPage()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);

			_stateMachine.SwitchToState<WelcomePagePresenter>();
		}
	}
}
