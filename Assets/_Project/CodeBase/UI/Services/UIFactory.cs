using _Project.CodeBase.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
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

        public async UniTask CreateUIRoot()
        {
            var prefab = await _assetProvider.InstantiateAsync(AssetPath.UIRootPath);
            _uiRoot = InstantiateRegistered(prefab).transform;
        }

        public async UniTask<GameObject> CreateHud()
        {
            var prefab = await _assetProvider.InstantiateAsync(AssetPath.HudPath);
            var hud = InstantiateRegistered(prefab, _uiRoot);
            _hud = hud.transform;
            return hud;
        }

        private GameObject InstantiateRegistered(GameObject prefab) => 
            Object.Instantiate(prefab);

        private GameObject InstantiateRegistered(GameObject prefab, Transform parent) => 
            Object.Instantiate(prefab, parent);
    }
}