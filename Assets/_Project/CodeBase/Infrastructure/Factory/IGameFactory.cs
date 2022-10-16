using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        HeroFacade HeroFacade { get; }

        void CreateHero();
        UniTask CreateInteractableSpawner();
        void AddProgressWatchers(GameObject go);
        
        void Cleanup();
        void WarmUp();
    }
}