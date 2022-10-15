using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IWindowService _windowService;

        public InitializeGameSceneState(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IPersistentProgressService persistentProgressService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
        }

        public async UniTask Enter()
        {
            await InitializeGameWorld();
            InformProgressReaders();
        }
        
        public void Exit() { }

        private async UniTask InitializeGameWorld()
        {
            var hero = InitializePlayer();
            await InitializeInteractableSpawner(hero);
            await InitializeUI(hero);
        }

        private async UniTask InitializeInteractableSpawner(GameObject hero) => 
            await _gameFactory.CreateInteractableSpawner(hero);

        private async UniTask InitializeUI(GameObject hero)
        {
            await CreateUI(hero);
            ConstructUI(hero);
        }

        private async UniTask CreateUI(GameObject hero)
        {
            await InitializeUIRoot();
            await InitializeHud();

            InitializeInventory();
            InitializeSettings(hero);
        }

        private void ConstructUI(GameObject hero)
        {
            _uiFactory.ConstructHud(hero);
            _uiFactory.ConstructInventoriesHolder(hero);
        }

        private GameObject InitializePlayer() => 
            _gameFactory.CreateHero();

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud() => 
            await _uiFactory.CreateHud();

        private void InitializeInventory() => 
             _uiFactory.CreateInventory();

        private void InitializeSettings(GameObject hero)
        {
            var settings = _uiFactory.CreateSettings(hero.GetComponent<HeroRotation>());
            _gameFactory.AddProgressWatchers(settings);
        }

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);
        }
    }
}