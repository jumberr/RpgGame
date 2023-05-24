using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Utils.Factory;
using Zenject;

namespace _Project.CodeBase.UI.Core
{
    public class UIRootFactory : ComponentFactory<UIRoot>
    {
        protected UIRootFactory(DiContainer container, IAssetProvider assetProvider) : base(container, assetProvider)
        {
        }
    }
}