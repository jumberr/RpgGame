using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Cam;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Elements.Hud;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;
using _Project.CodeBase.UI.Windows.DeathScreen;
using _Project.CodeBase.UI.Windows.Inventory;
using _Project.CodeBase.UI.Windows.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;
        private readonly InputService _inputService;
        private readonly SceneLoader _sceneLoader;
        private Transform _uiRoot;
        private GameObject _hud;
        private InventoriesHolderUI _inventoriesHolder;
        private ActorUI _actorUI;
        private InventoryUI _inventoryUI;

        public UIFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            InputService inputService,
            IWindowService windowService,
            SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _inputService = inputService;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
        }

        public async UniTask CreateUIRoot()
        {
            _uiRoot = (await _assetProvider.InstantiateAsync(AssetPath.UIRootPath)).transform;
            _inventoriesHolder = _uiRoot.GetComponent<InventoriesHolderUI>();
        }

        public async UniTask CreateHud()
        {
            _hud = await _assetProvider.InstantiateAsync(AssetPath.HudPath, parent: _uiRoot);

            SetupHud();
            //SetupWindowButtons();
        }

        public void SetupWindowService() => 
            _windowService.Setup();

        public async void CreateDeathScreen()
        {
            var deathScreen = await _assetProvider.InstantiateComponentAsync<DeathScreen>(AssetPath.DeathScreen, parent: _uiRoot);
            deathScreen.Construct(_sceneLoader, _staticDataService);
        }

        public void CreateInventory()
        {
            var prefab = _staticDataService.ForWindow(WindowId.Inventory).Prefab;
            _inventoryUI = Object.Instantiate(prefab, _uiRoot) as InventoryUI;
            AddWindow(_inventoryUI, WindowId.Inventory);
        }

        public GameObject CreateSettings(HeroCamera camera)
        {
            var prefab = _staticDataService.ForWindow(WindowId.Settings).Prefab;
            var settings = Object.Instantiate(prefab, _uiRoot) as SettingsUI;
            settings.Construct(camera);
            
            AddWindow(settings, WindowId.Settings);
            return settings.gameObject;
        }

        public void ConstructHud(HeroFacade facade)
        {
            _actorUI = _hud.GetComponent<ActorUI>();
            _actorUI.Construct(_inputService, facade.Health, facade.Ammo, facade.WeaponController, facade.HeroState, facade.Interaction);
        }

        public void ConstructInventoriesHolder(HeroFacade facade) => 
            _inventoriesHolder.Construct(_actorUI.HotBar, _inventoryUI, facade.Inventory);

        private void SetupHud()
        {
            var rectTransform = _hud.GetComponent<RectTransform>();
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
        }

        private void AddWindow(WindowBase window, WindowId id) => 
            _windowService.AddWindow(window, id);
        
        //private void SetupWindowButtons()
        //{
        //    foreach (var button in _hud.GetComponentsInChildren<OpenWindowButton>()) 
        //        button.Construct(_windowService);
        //}
    }
}