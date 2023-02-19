using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Core;
using _Project.CodeBase.UI.Services.Windows;
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
        private DeathScreen _deathScreen;

        public UIFactory(
            UIRoot.Factory uiRootFactory,
            WindowBase.Factory windowFactory,
            Screen.Factory screenFactory)
        {
            _uiRootFactory = uiRootFactory;
            _windowFactory = windowFactory;
            _screenFactory = screenFactory;
        }

        public async UniTask CreateUI()
        {
            await CreateUIRoot();
            await CreateScreens();
            CreateWindows();
        }

        public void ShowDeathScreen() => 
            _deathScreen.Show();

        private async UniTask CreateUIRoot()
        {
            _uiRoot = (await _uiRootFactory.Create(AssetPath.UIRootPath)).transform;
            _inventoriesHolder = _uiRoot.GetComponent<InventoriesHolderUI>();
        }

        private async UniTask CreateScreens()
        {
            await CreateHud();
            await CreateDeathScreen();
        }

        private void CreateWindows()
        {
            CreateInventory();
            CreateSettings();
        }

        private async UniTask CreateHud() => 
            _actorUI = (ActorUI) await _screenFactory.Create(AssetPath.HudPath, _uiRoot);

        private async UniTask CreateDeathScreen() => 
            _deathScreen = (DeathScreen) await _screenFactory.Create(AssetPath.DeathScreen, _uiRoot);

        private void CreateInventory() => 
            _inventoryWindow = (InventoryWindow) _windowFactory.Create(WindowId.Inventory, _uiRoot);

        private void CreateSettings() => 
            _windowFactory.Create(WindowId.Settings, _uiRoot);

        public void ConstructInventoriesHolder(HeroFacade facade) => 
            _inventoriesHolder.Construct(_actorUI.HotBar, _inventoryWindow, facade.Inventory);
    }
}