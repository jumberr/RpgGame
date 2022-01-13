using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private ProjectSettings projectSettings;
        
        public override void InstallBindings()
        {
            RegisterCompositionRoot();
            RegisterBootstrapState();

            RegisterInitializeSceneState();
        }

        private void RegisterCompositionRoot()
        {
            Container
                .Bind<GameStateMachine>()
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
                .FromInstance(projectSettings)
                .AsSingle();
        }

        private void RegisterBootstrapState()
        {
            Container
                .Bind<LoadingCurtain>()
                .FromComponentInNewPrefab(projectSettings.LoadingCurtain)
                .AsSingle();

            Container
                .Bind<SceneLoader>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle();
        }

        private void RegisterInitializeSceneState()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}
