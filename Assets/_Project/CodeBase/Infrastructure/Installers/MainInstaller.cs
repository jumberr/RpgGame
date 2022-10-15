using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Elements.Hud;
using _Project.CodeBase.UI.Services;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _hudPrefab;

        public override void InstallBindings()
        {
            //Container
            //    .Bind<InputService>()
            //    .AsSingle();
            //
            //Container
            //    .Bind<IGameFactory>()
            //    .To<GameFactory>()
            //    .AsSingle();
            //
            //Container
            //    .Bind<IUIFactory>()
            //    .To<UIFactory>()
            //    .AsSingle();
//
            //Container
            //    .BindFactory<ActorUI, ActorUI.Factory>()
            //    .FromComponentInNewPrefab(_hudPrefab)
            //    .AsSingle();
            //
            //Container
            //    .BindFactory<HeroFacade, HeroFacade.Factory>()
            //    .FromComponentInNewPrefab(_player);
            //
            //Container
            //    .Bind<HeroFacade>()
            //    .FromFactory<HeroFacade.Factory>();
        }
    }
}