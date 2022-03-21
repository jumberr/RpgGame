using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroScoping : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private InputService _inputService;
        
        private void Start()
        {
            _inputService.OnScope += ScopeHandling;
        }

        public void ScopeHandling(bool value)
        {
            if (value)
                Scope();
            else
            {
                UnScope();
            }
        }

        private void Scope()
        {
            throw new System.NotImplementedException();
        }

        private void UnScope()
        {
            throw new System.NotImplementedException();
        }
    }
}