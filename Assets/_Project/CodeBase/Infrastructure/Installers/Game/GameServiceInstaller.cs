using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class GameServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<InputService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameFactory>()
                .AsSingle();

            Container
                .Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();

            Container
                .Bind<ISaveLoadService>()
                .To<JsonSaveLoadService>()
                .AsSingle();
        }
    }
}