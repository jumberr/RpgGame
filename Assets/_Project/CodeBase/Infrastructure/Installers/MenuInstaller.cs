using Zenject;

namespace _Project.CodeBase.Infrastructure
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