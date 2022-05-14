using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        private State _prevState;
        private State _state;
        
        public State PreviousState => _prevState;
        public State CurrentState => _state;

        private void Start() => 
            ChangeState(State.None);

        public void Enter(State newState, Action onStart = null, Action onComplete = null)
        {
            onStart?.Invoke();
            ChangeState(newState);
            onComplete?.Invoke();
        }

        private void ChangeState(State newState)
        {
            _prevState = _state;
            _state = newState;
        }
    }
}