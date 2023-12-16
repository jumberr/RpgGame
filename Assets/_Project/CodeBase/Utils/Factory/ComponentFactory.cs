using _Project.CodeBase.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Utils.Factory
{
    public class ComponentFactory<T> : IFactory<string, UniTask<T>> where T : Component
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;

        protected ComponentFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public virtual async UniTask<T> Create(string path)
        {
            var prefab = await _assetProvider.LoadComponent<T>(path);
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }
    }
}