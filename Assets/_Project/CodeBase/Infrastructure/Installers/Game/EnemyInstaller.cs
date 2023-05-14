using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Enemy;
using _Project.CodeBase.Logic.Enemy.FSM;
using _Project.CodeBase.Logic.Enemy.FSM.States;
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
                .BindMemoryPool<EnemyFacade, EnemyObjectPool>()
                .FromFactory<EnemyFactory>();

            Container
                .Bind<EnemyProvider>()
                .AsSingle();

            Container
                .Bind<AIObserver>()
                .AsSingle();

            Container
                .Bind<AIStateMachine>()
                .AsTransient();

            Container
                .BindInterfacesAndSelfTo<IdleState>()
                .AsTransient();
            
            Container
                .BindInterfacesAndSelfTo<ChaseState>()
                .AsTransient();
            
            Container
                .BindInterfacesAndSelfTo<CombatState>()
                .AsTransient();

            Container
                .BindInterfacesAndSelfTo<DeathState>()
                .AsTransient();
        }
    }
}