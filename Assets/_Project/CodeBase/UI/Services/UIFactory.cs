using System.Collections.Generic;
using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Elements.Hud;
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
        private readonly IWindowService _windowService;

        private readonly Dictionary<WindowId, WindowBase> _windows = new Dictionary<WindowId, WindowBase>();
        private Transform _uiRoot;
        private GameObject _hud;

        public UIFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
            _windowService = new WindowService(this);
        }

        public async UniTask CreateUIRoot()
        {
            var uiRoot = await _assetProvider.InstantiateAsync(AssetPath.UIRootPath);
            _uiRoot = uiRoot.transform;
        }

        public async UniTask CreateHud()
        {
            _hud = await _assetProvider.InstantiateAsync(AssetPath.HudPath, _uiRoot);
            SetupWindowButtons();
        }

        public async void CreateDeathScreen()
        {
            var deathScreenGo = await _assetProvider.InstantiateAsync(AssetPath.DeathScreen, _uiRoot);
            var deathScreen = deathScreenGo.GetComponent<DeathScreen>();
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

        public async void ConstructActorUI(GameObject hero)
        {
            var actorUI = _hud.GetComponentInChildren<ActorUI>();
            await actorUI.Construct(hero.GetComponent<HeroHealth>(), hero.GetComponent<HeroAmmo>(),
                hero.GetComponent<WeaponController>(), hero.GetComponent<InputService>(),
                hero.GetComponent<HeroState>(), hero.GetComponent<Interaction>(), hero.GetComponent<HeroInventory>());
        }

        public void OpenWindow(WindowId id) => 
            _windows[id].Show();

        public void HideWindow(WindowId id) => 
            _windows[id].Hide();

        private void SetupWindowButtons()
        {
            foreach (var button in _hud.GetComponentsInChildren<OpenWindowButton>()) 
                button.Construct(_windowService);
        }

        private void AddWindow(WindowBase window, WindowId id) => 
            _windows.Add(id, window);
    }
}