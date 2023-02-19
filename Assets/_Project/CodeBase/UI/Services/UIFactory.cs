using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Core;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Screen = _Project.CodeBase.UI.Screens.Screen;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly UIRoot.Factory _uiRootFactory;
        private readonly WindowBase.Factory _windowFactory;
        private readonly Screen.Factory _screenFactory;

        private Transform _uiRoot;
        private InventoriesHolderUI _inventoriesHolder;
        private ActorUI _actorUI;
        private InventoryWindow _inventoryWindow;

        public UIFactory(
            UIRoot.Factory uiRootFactory,
            WindowBase.Factory windowFactory,
            Screen.Factory screenFactory)
        {
            _uiRootFactory = uiRootFactory;
            _windowFactory = windowFactory;
            _screenFactory = screenFactory;
        }

        public async UniTask CreateUIRoot()
        {
            _uiRoot = (await _uiRootFactory.Create(AssetPath.UIRootPath)).transform;
            _inventoriesHolder = _uiRoot.GetComponent<InventoriesHolderUI>();
        }

        public async UniTask CreateHud() => 
            _actorUI = (ActorUI) await _screenFactory.Create(AssetPath.HudPath, _uiRoot);

        public async void CreateDeathScreen() => 
            await _screenFactory.Create(AssetPath.DeathScreen, _uiRoot);

        public void CreateInventory() => 
            _inventoryWindow = (InventoryWindow) _windowFactory.Create(WindowId.Inventory, _uiRoot);

        public SettingsUI CreateSettings() => 
            (SettingsUI) _windowFactory.Create(WindowId.Settings, _uiRoot);

        public void ConstructInventoriesHolder(HeroFacade facade) => 
            _inventoriesHolder.Construct(_actorUI.HotBar, _inventoryWindow, facade.Inventory);
    }
}