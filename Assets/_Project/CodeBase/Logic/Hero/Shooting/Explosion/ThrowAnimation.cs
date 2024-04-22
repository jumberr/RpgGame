using System;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic.Hero
{
    public class ThrowAnimation : IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly InputService _inputService;
        private HeroAnimator _heroAnimator;
        private bool _prepared;


        public ThrowAnimation(IGameFactory gameFactory, InputService inputService)
        {
            _gameFactory = gameFactory;
            _inputService = inputService;
            _gameFactory.HeroCreated += Init;
            _inputService.ScopeAction.Event += PrepareThrowAnimation;
        }

        public void Dispose()
        {
            _gameFactory.HeroCreated -= Init;
            _inputService.ScopeAction.Event += PrepareThrowAnimation;
        }

        public async UniTask HandleThrowAnimation()
        {
            // todo check case when undo throwing
            await _heroAnimator.Throw();
        }

        private void Init(HeroFacade facade) => 
            _heroAnimator = facade.Animator;

        private void PrepareThrowAnimation(bool value)
        {
            if (value)
            {
                if (_prepared) return;
                _prepared = true;
                _heroAnimator.PrepareThrowing();
            }
            else
                _prepared = false;
        }
    }
}