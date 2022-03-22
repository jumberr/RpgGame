using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        private EHeroState _prevState;
        private EHeroState _state;
        
        public EHeroState PreviousState => _prevState;
        public EHeroState CurrentState => _state;

        private void Start() => 
            ChangeState(EHeroState.None);

        public void Enter(EHeroState newState, Action onStart = null, Action onComplete = null)
        {
            onStart?.Invoke();
            ChangeState(newState);
            onComplete?.Invoke();
        }

        private void ChangeState(EHeroState newState)
        {
            _prevState = _state;
            _state = newState;
        }
    }
}