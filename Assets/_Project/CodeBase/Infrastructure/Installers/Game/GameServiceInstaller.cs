using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.UI.Screens;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Screen = _Project.CodeBase.UI.Screens.Screen;

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
                .BindFactory<WindowId, Transform, WindowBase, WindowBase.Factory>()
                .FromFactory<WindowFactory>();
            
            Container
                .BindFactory<string, Transform, UniTask<Screen>, Screen.Factory>()
                .FromFactory<ScreenFactory>();
            
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

            Container
                .Bind<IWindowService>()
                .To<WindowService>()
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