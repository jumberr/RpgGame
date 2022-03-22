using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroScoping : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;

        private bool _isScoping;
        
        private void Start() => 
            _inputService.OnScope += ScopeHandling;

        private void OnDisable() => 
            _inputService.OnScope -= ScopeHandling;

        public void UnScope()
        {
            _heroAnimator.Scope(false);
            _state.Enter(EHeroState.None);
            _isScoping = false;
        }

        private void ScopeHandling()
        {
            if (!_isScoping)
                Scope();
            else
                UnScope();
        }

        private void Scope()
        {
            if (_state.CurrentState == EHeroState.Reload) return;
            _state.Enter(EHeroState.Scoping);
            _heroAnimator.Scope(true);
            _isScoping = true;
        }
    }
}