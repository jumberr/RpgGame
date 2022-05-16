using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        public event Action<State> OnChangeState;
        
        private State _prevState;
        private State _state;
        
        public State PreviousState => _prevState;
        public State CurrentState => _state;

        private void Start() => 
            ChangeState(State.None);

        public void Enter(State newState)
        {
            ChangeState(newState);
            OnChangeState?.Invoke(_state);
        }

        private void ChangeState(State newState)
        {
            _prevState = _state;
            _state = newState;
        }
    }
}