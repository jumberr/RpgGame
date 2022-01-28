using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        private Transform _uiRoot;
        private Transform _hud;

        public UIFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

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
    }
}