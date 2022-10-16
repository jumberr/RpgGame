using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly HeroFacade.Factory _heroFactory;
        private readonly InputService _inputService;
        private InteractableSpawner _interactableSpawner;
        private HeroFacade _heroFacade;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public HeroFacade HeroFacade => _heroFacade;

        private GameObject HeroGameObject { get; set; }

        public GameFactory(
            IAssetProvider assetProvider,
            IUIFactory uiFactory,
            IStaticDataService staticDataService,
            HeroFacade.Factory heroFactory,
            InputService inputService)
        {
            _heroFactory = heroFactory;
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _inputService = inputService;
        }

        public void CreateHero()
        {
            _heroFacade = _heroFactory.Create();
            HeroGameObject = _heroFacade.gameObject;
            RegisterProgressWatchers(HeroGameObject);
            _heroFacade.Construct(_inputService, _staticDataService, _uiFactory.CreateDeathScreen);
        }

        public async UniTask CreateInteractableSpawner()
        {
            _interactableSpawner = await _assetProvider.InstantiateComponentAsync<InteractableSpawner>(AssetPath.InteractableSpawner);
            _interactableSpawner.Construct(_heroFacade.Inventory);
        }

        public void AddProgressWatchers(GameObject go) => 
            RegisterProgressWatchers(go);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assetProvider.CleanUp();
        }

        public void WarmUp() { }

        private void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in go.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateAndRegister(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab);
            RegisterProgressWatchers(instance);
            return instance;
        }
    }
}