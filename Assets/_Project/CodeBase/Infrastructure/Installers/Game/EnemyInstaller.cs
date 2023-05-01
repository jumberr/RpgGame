using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Enemy;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyFacade, EnemyFacade.Factory>()
                .FromFactory<EnemyFactory>();

            Container
                .Bind<EnemyProvider>()
                .AsSingle();

            Container
                .Bind<AIObserver>()
                .AsSingle();
        }
    }
}