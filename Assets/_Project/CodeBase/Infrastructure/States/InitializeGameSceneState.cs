using System;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.UI.Services;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly GameStateComponentInitializer _componentInitializer;

        public event Action OnWorldInitialized;
        private event Action OnProgressWatchersInformed;

        public InitializeGameSceneState(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IPersistentProgressService persistentProgressService,
            GameStateComponentInitializer componentInitializer)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
            _componentInitializer = componentInitializer;

            OnWorldInitialized += _componentInitializer.RegisterProgressWatchers;
            OnProgressWatchersInformed += _componentInitializer.InitializeComponents;
        }

        ~InitializeGameSceneState()
        {
            OnWorldInitialized -= _componentInitializer.RegisterProgressWatchers;
            OnProgressWatchersInformed -= _componentInitializer.InitializeComponents;
        }

        public async UniTask Enter()
        {
            await InitializeGameWorld();
            InformProgressReaders();
        }
        
        public void Exit() { }

        private async UniTask InitializeGameWorld()
        {
            await InitializePlayer();
            InitializeAI();
            await InitializeUI();
            OnWorldInitialized?.Invoke();
        }

        private async UniTask InitializePlayer() => 
            await _gameFactory.CreateHero();

        private void InitializeAI() => 
            _gameFactory.InitializeAI();

        private async UniTask InitializeUI() => 
            await _uiFactory.CreateUI();

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);

            OnProgressWatchersInformed?.Invoke();
        }
    }
}