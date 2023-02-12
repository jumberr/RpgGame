using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Interaction;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private InteractableSpawner _interactableSpawner;

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
                .BindFactory<HeroFacade, HeroFacade.Factory>()
                .FromComponentInNewPrefab(_player);

            Container
                .Bind<InteractableSpawner>()
                .FromInstance(_interactableSpawner)
                .AsSingle();
        }
    }
}