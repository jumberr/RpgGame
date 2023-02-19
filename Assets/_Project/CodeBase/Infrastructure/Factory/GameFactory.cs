using System;
using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory, IDisposable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly HeroFacade.Factory _heroFactory;
        private readonly MapStorage _mapStorage;
        private HeroFacade _heroFacade;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public HeroFacade HeroFacade => _heroFacade;
        
        public GameFactory(
            IAssetProvider assetProvider,
            IUIFactory uiFactory,
            HeroFacade.Factory heroFactory,
            MapStorage mapStorage)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _heroFactory = heroFactory;
            _mapStorage = mapStorage;
        }

        public void Dispose() => 
            Cleanup();

        public async UniTask CreateHero()
        {
            _heroFacade = await _heroFactory.Create(AssetPath.HeroPath);
            RegisterProgressWatchers(_heroFacade.gameObject);
            _heroFacade.Setup(_uiFactory.CreateDeathScreen);
        }

        public void SetupComponents() => 
            Register(_mapStorage);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assetProvider.Cleanup();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in go.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}