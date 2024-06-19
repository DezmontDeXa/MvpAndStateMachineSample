using System;
using Zenject;

namespace Infrastructure
{
	public class StateFactory
	{
		private readonly DiContainer _diContainer;

		public StateFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
		}

		public IState Create<TStateType>() where TStateType : IState
		{
			return _diContainer.Resolve<TStateType>();
		}

		public IState Create(Type stateType)
		{
			return (IState)_diContainer.Resolve(stateType);
		}
	}
}
