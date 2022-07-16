using System.Collections.Generic;
using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;
using _Project.CodeBase.UI.Windows.DeathScreen;
using _Project.CodeBase.UI.Windows.Inventory;
using _Project.CodeBase.UI.Windows.Settings;
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

        private readonly Dictionary<WindowId, WindowBase> _windows = new Dictionary<WindowId, WindowBase>();
        private Transform _uiRoot;
        private Transform _hud;

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
            var inventory = Object.Instantiate(prefab, _uiRoot) as InventoryUI;
            inventory.Construct(hero.GetComponent<HeroInventory>());
            
            AddWindow(inventory, WindowId.Inventory);
        }

        public GameObject CreateSettings(HeroRotation rotation)
        {
            var prefab = _staticDataService.ForWindow(WindowId.Settings).Prefab;
            var settings = Object.Instantiate(prefab, _uiRoot) as SettingsUI;
            settings.Construct(rotation);
            
            AddWindow(settings, WindowId.Settings);
            return settings.gameObject;
        }

        public void SetupWindowButtons(IWindowService windowService)
        {
            foreach (var button in _hud.GetComponentsInChildren<OpenWindowButton>()) 
                button.Construct(windowService);
        }

        public void OpenWindow(WindowId id) => 
            _windows[id].Show();

        public void HideWindow(WindowId id) => 
            _windows[id].Hide();

        private void AddWindow(WindowBase window, WindowId id) => 
            _windows.Add(id, window);
    }
}