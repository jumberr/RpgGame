using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.States;
using _Project.CodeBase.Services;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.UI.Services;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private ProjectSettings _projectSettings;
        
        public override void InstallBindings()
        {
            RegisterCompositionRoot();
            RegisterBootstrapState();
            RegisterLoadProgressState();
            RegisterInitializeSceneState();
        }

        private void RegisterCompositionRoot()
        {
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();

            Container.Bind<IExitableState>()
                .To(x => x.AllNonAbstractClasses())
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();

            Container
                .Bind<ProjectSettings>()
                .FromInstance(_projectSettings)
                .AsSingle();
        }

        private void RegisterBootstrapState()
        {
            Container
                .Bind<LoadingCurtain>()
                .FromComponentInNewPrefab(_projectSettings.LoadingCurtain)
                .AsSingle();

            Container
                .Bind<SceneLoader>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle();
        }

        private void RegisterLoadProgressState()
        {
            Container
                .Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();

            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }

        private void RegisterInitializeSceneState()
        {
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
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
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}
