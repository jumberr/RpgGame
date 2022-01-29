using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.UI.Windows.DeathScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;

        private Transform _uiRoot;
        private Transform _hud;

        public UIFactory(
            IAssetProvider assetProvider,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _assetProvider = assetProvider;
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
    }
}