using _Project.CodeBase.Logic.Hero;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class ThrowInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ItemThrower>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ThrowAnimation>()
                .AsSingle();

            Container
                .Bind<TrajectoryDrawer>()
                .AsSingle();
        }
    }
}