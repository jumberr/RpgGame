using _Project.CodeBase.Data;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<MapStorage>()
                .AsSingle();
            
            Container
                .Bind<ItemStorage>()
                .AsSingle();
        }
    }
}