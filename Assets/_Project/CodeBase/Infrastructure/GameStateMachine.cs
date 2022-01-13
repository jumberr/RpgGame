using System;
using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.States;

namespace _Project.CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _actualState;
        
        public GameStateMachine(List<IExitableState> states) => 
            _states = states.ToDictionary(x => x.GetType(), x => x);

        public void Enter<TState>() where TState : class, IState => 
            ChangeState<TState>().Enter();

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _actualState?.Exit();
            var newState = GetState<TState>();
            _actualState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}