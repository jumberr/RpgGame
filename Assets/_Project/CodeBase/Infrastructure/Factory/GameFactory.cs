using System.Collections.Generic;
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private GameObject HeroGameObject { get; set; }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public async UniTask<GameObject> CreateHero(Vector3 at)
        {
            var prefab = await _assetProvider.InstantiateAsync(AssetPath.HeroPath);
            HeroGameObject = InstantiateRegistered(prefab, at);
            var playerMovement = HeroGameObject.GetComponent<HeroMovement>();
            var playerAnimator = HeroGameObject.GetComponent<HeroAnimator>();
            
            playerMovement.Construct(playerAnimator);

            return HeroGameObject;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            //_assets.CleanUp();
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

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            var instance = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(instance);
            return instance;
        }
    }
}