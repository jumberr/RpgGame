using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Hero.Animation.States;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Animation
{
    public class AnimationStateMachine : IAnimationStateMachine
    {
        private readonly Dictionary<Type, IAnimationState> _states;
        private IAnimationState _actualState;
        
        public AnimationStateMachine(Animator animator)
        {
            _states = new Dictionary<Type, IAnimationState>
            {
                {typeof(IdleAnimationState), new IdleAnimationState()},
                {typeof(WalkingAnimationState), new WalkingAnimationState()},
                {typeof(RunAnimationState), new RunAnimationState()}
            };

            Construct(animator);
        }

        private void Construct(Animator animator)
        {
            foreach (var state in _states) 
                state.Value.Construct(animator);
        }
        
        public EAnimationState GetStateName() => 
            _actualState.StateName;

        public void Enter<TState>() where TState : class, IAnimationState => 
            ChangeState<TState>().Enter();

        private TState ChangeState<TState>() where TState : class, IAnimationState
        {
            _actualState?.Exit();
            var newState = GetState<TState>();
            _actualState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, IAnimationState => 
            _states[typeof(TState)] as TState;
    }
}