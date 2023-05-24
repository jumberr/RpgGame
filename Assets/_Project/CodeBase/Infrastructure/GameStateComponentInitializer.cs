using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows.Settings;

namespace _Project.CodeBase.Infrastructure
{
    public class GameStateComponentInitializer
    {
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;
        private readonly MapStorage _mapStorage;
        private readonly InteractableSpawner _interactableSpawner;
        private readonly InventoriesHolderUI _inventoriesHolder;

        public GameStateComponentInitializer(
            IGameFactory gameFactory,
            IWindowService windowService,
            MapStorage mapStorage,
            InteractableSpawner interactableSpawner,
            InventoriesHolderUI inventoriesHolder)
        {
            _windowService = windowService;
            _gameFactory = gameFactory;
            _mapStorage = mapStorage;
            _interactableSpawner = interactableSpawner;
            _inventoriesHolder = inventoriesHolder;
        }

        public void InitializeComponents()
        {
            _interactableSpawner.Initialize();
            _mapStorage.Initialize();
            _inventoriesHolder.Initialize();
        }

        public void RegisterProgressWatchers()
        {
            _gameFactory.Register(_mapStorage);
            _gameFactory.Register((SettingsUI) _windowService.GetWindow(WindowId.Settings));
        }
    }
}