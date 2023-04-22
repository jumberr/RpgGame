using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.UI.Panels;
using _Project.CodeBase.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI.Screens
{
    public class ScreenFactory : PanelFactory, IFactory<string, Transform, UniTask<Screen>>
    {
        private readonly IAssetProvider _assetProvider;

        public ScreenFactory(DiContainer container, IAssetProvider assetProvider) : base(container) => 
            _assetProvider = assetProvider;

        public async UniTask<Screen> Create(string param, Transform parent)
        {
            var screenPrefab = await _assetProvider.LoadComponent<Screen>(param);
            var screen = Container.InstantiatePrefabForComponent<Screen>(screenPrefab, parent);
            ResizeWindow(screen.transform);
            return screen;
        }
    }
}