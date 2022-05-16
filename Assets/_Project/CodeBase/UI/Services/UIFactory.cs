using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Elements.Inventory;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Services.Windows.Inventory;
using _Project.CodeBase.UI.Windows.DeathScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;

        private Transform _uiRoot;
        private Transform _hud;
        
        private InventoryUI _inventory;

        public UIFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public async UniTask CreateUIRoot()
        {
            var uiRoot = await _assetProvider.InstantiateAsync(AssetPath.UIRootPath);
            _uiRoot = uiRoot.transform;
        }

        public async UniTask<GameObject> CreateHud()
        {
            var hud = await _assetProvider.InstantiateAsync(AssetPath.HudPath, _uiRoot);
            _hud = hud.transform;
            return hud;
        }

        public async void CreateDeathScreen()
        {
            var gameObject = await _assetProvider.InstantiateAsync(AssetPath.DeathScreen, _uiRoot);
            var deathScreen = gameObject.GetComponent<DeathScreen>();
            deathScreen.Construct(_gameStateMachine.Value);
        }

        public void CreateInventory(GameObject hero)
        {
            var prefab = _staticDataService.ForWindow(WindowId.Inventory).Prefab;
            _inventory = Object.Instantiate(prefab, _uiRoot) as InventoryUI;
            _inventory.Construct(hero.GetComponent<HeroInventory>());
        }

        public void SetupWindowButtons(IWindowService windowService)
        {
            foreach (var button in _hud.GetComponentsInChildren<OpenWindowButton>()) 
                button.Construct(windowService);
        }

        public void OpenInventory() => 
            _inventory.gameObject.SetActive(true);
    }
}