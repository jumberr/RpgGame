using _Project.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        private Transform _uiRoot;
        private Transform _hud;

        public UIFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;

        public void CreateHud() => 
            _hud = _assetProvider.Instantiate(AssetPath.HudPath, _uiRoot).transform;
    }
}