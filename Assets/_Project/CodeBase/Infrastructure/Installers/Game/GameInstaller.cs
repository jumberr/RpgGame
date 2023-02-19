using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Interaction;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInitializable>()
                .To<GameScene>()
                .AsSingle();
            
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();

            Container.Bind<IExitableState>()
                .To(x => x.AllNonAbstractClasses())
                .AsSingle();

            Container
                .BindFactory<string, UniTask<HeroFacade>, HeroFacade.Factory>()
                .FromFactory<HeroFacadeFactory>();

            Container
                .BindInterfacesAndSelfTo<InteractableSpawner>()
                .AsSingle();
        }
    }
}