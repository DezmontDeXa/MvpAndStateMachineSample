using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
	public abstract class StateMachine<TState> where TState : IState
	{
		private readonly StateFactory _stateFactory;
		private IState _currentState;
		private Stack<IState> _navigationHistory = new Stack<IState>();

		public StateMachine(StateFactory stateFactory)
		{
			_stateFactory = stateFactory;
		}

		public void Back()
		{
			if (_currentState != null)
				_currentState.Back();

			var state = _navigationHistory.Pop();

			state.Restore();

			_currentState = state;
		}

		public void SwitchToState<TTargetState>() where TTargetState : IState
		{
			var nextState = _stateFactory.Create<TTargetState>();

			SwitchToState(nextState);
		}

		public void SwitchToState(Type stateType)
		{
			var nextState = _stateFactory.Create(stateType);

			SwitchToState(nextState);
		}

		public virtual void SwitchToState(IState state)
		{
			if (_currentState != null)
			{
				_currentState.Exit();

				_navigationHistory.Push(_currentState);
			}

			// TODO: Fix cycles

			state.Enter();

			_currentState = state;
		}
	}
}
