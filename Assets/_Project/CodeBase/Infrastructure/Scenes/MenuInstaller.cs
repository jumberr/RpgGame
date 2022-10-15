using Zenject;

namespace _Project.CodeBase.Infrastructure.Scenes
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInitializable>()
                .To<MenuScene>()
                .AsSingle();
        }
    }
}