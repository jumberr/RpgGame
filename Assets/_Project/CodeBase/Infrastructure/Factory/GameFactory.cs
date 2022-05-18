using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly IPoolManager _poolManager;
        private readonly IStaticDataService _staticDataService;
        private InteractableSpawner _interactableSpawner;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public IPoolManager PoolManager => _poolManager;
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        
        private GameObject HeroGameObject { get; set; }

        public GameFactory(
            IAssetProvider assetProvider,
            IUIFactory uiFactory,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _poolManager = new PoolManager();
        }

        public async UniTask<GameObject> CreateHero(Vector3 at)
        {
            HeroGameObject = await _assetProvider.InstantiateAsync(AssetPath.HeroPath, at);
            RegisterProgressWatchers(HeroGameObject);

            var heroShooting = HeroGameObject.GetComponent<HeroShooting>();
            heroShooting.Construct(_poolManager);

            var inventory = HeroGameObject.GetComponent<HeroInventory>();
            inventory.Construct(_staticDataService);
            
            var heroDeath = HeroGameObject.GetComponent<HeroDeath>();
            heroDeath.ZeroHealth += _uiFactory.CreateDeathScreen;
            
            return HeroGameObject;
        }

        public async UniTask CreateInteractableSpawner(GameObject hero)
        {
            _interactableSpawner = (await _assetProvider.InstantiateAsync(AssetPath.InteractableSpawner))
                .GetComponent<InteractableSpawner>();
            var inventory = hero.GetComponent<HeroInventory>();
            _interactableSpawner.Construct(inventory);
            inventory.Construct(_interactableSpawner);
        }

        public void Cleanup()
        {
            _poolManager.CleanUp();
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