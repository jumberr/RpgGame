using System.Collections.Generic;
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        
        private GameObject HeroGameObject { get; set; }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(
            IAssetProvider assetProvider,
            IUIFactory uiFactory)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
        }

        public async UniTask<GameObject> CreateHero(Vector3 at)
        {
            HeroGameObject = await _assetProvider.InstantiateAsync(AssetPath.HeroPath, at);
            RegisterProgressWatchers(HeroGameObject);
            
            //var heroMovement = HeroGameObject.GetComponent<HeroMovement>();
            //var heroAnimator = HeroGameObject.GetComponent<HeroAnimator>();
            var heroHealth = HeroGameObject.GetComponent<HeroHealth>();

            heroHealth.ZeroHealth += _uiFactory.CreateDeathScreen;
            
            //heroMovement.Construct(heroAnimator);

            return HeroGameObject;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assetProvider.CleanUp();
        }

        public void WarmUp()
        {
            //
        }

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

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab);
            RegisterProgressWatchers(instance);
            return instance;
        }
    }
}