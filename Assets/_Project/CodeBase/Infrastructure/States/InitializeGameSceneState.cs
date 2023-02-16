using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IWindowService _windowService;
        private readonly MapStorage _mapStorage;

        public InitializeGameSceneState(
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IPersistentProgressService persistentProgressService,
            MapStorage mapStorage)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
            _mapStorage = mapStorage;
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
            SetupComponents();
            await InitializeUI(_gameFactory.HeroFacade);
        }

        private void SetupComponents()
        {
            _gameFactory.Register(_mapStorage);
            _gameFactory.SetupInteractableSpawner();
        }

        private async UniTask InitializeUI(HeroFacade facade)
        {
            await CreateUI();
            ConstructUI(facade);
        }

        private async UniTask CreateUI()
        {
            await InitializeUIRoot();
            await InitializeHud();

            InitializeInventory();
            InitializeSettings();
        }

        private void ConstructUI(HeroFacade facade) => 
            _uiFactory.ConstructInventoriesHolder(facade);

        private async UniTask InitializePlayer() => 
            await _gameFactory.CreateHero();

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud() => 
            await _uiFactory.CreateHud();

        private void InitializeInventory() => 
             _uiFactory.CreateInventory();

        private void InitializeSettings()
        {
            var settings = _uiFactory.CreateSettings();
            _gameFactory.Register(settings);
        }

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);
        }
    }
}