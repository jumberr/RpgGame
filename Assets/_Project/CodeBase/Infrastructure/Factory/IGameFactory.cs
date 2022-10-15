using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        MainPoolManager PoolManager { get; }

        GameObject CreateHero();
        UniTask CreateInteractableSpawner(GameObject hero);
        void AddProgressWatchers(GameObject go);
        
        void Cleanup();
        void WarmUp();
    }
}