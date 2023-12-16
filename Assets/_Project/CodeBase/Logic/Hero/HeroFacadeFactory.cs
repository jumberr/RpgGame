using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Utils.Factory;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroFacadeFactory : ComponentFactory<HeroFacade>
    {
        public HeroFacadeFactory(DiContainer container, IAssetProvider assetProvider) : base(container, assetProvider)
        {
        }
    }
}