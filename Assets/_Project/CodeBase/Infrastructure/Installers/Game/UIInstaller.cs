using _Project.CodeBase.UI;
using _Project.CodeBase.UI.Core;
using _Project.CodeBase.UI.Screens;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Screen = _Project.CodeBase.UI.Screens.Screen;

namespace _Project.CodeBase.Infrastructure
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, UniTask<UIRoot>, UIRoot.Factory>()
                .FromFactory<UIRootFactory>();
            
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
                .Bind<InventoriesHolderUI>()
                .AsSingle()
                .NonLazy();
        }
    }
}