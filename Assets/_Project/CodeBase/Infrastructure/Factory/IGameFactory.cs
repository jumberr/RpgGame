using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        HeroFacade HeroFacade { get; }

        void CreateHero();
        void SetupInteractableSpawner();
        void AddProgressWatchers(GameObject go);
        void Register(ISavedProgressReader progressReader);

        void Cleanup();
    }
}