using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Scenes
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;

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
                .Bind<InputService>()
                .AsSingle();

            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

            Container
                .Bind<IWindowService>()
                .To<WindowService>()
                .AsSingle();
            
            Container
                .BindFactory<HeroFacade, HeroFacade.Factory>()
                .FromComponentInNewPrefab(_player);
        }
    }
}