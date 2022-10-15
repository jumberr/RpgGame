using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.SaveLoad;
using _Project.CodeBase.Infrastructure.Scenes;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ProjectSettings _projectSettings;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IInitializable>()
                .To<Bootstrap>()
                .AsSingle();
            
            Container
                .Bind<InitialScene>()
                .AsSingle();
            
            Container
                .Bind<ProjectSettings>()
                .FromInstance(_projectSettings)
                .AsSingle();
            
            BindServices();
        }

        private void BindServices()
        {
            Container
                .Bind<LoadingCurtain>()
                .FromComponentInNewPrefab(_projectSettings.LoadingCurtain)
                .AsSingle();

            Container
                .Bind<SceneLoader>()
                .AsSingle();

            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();

            Container
                .Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();

            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();

            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}
