using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        
        UniTask<GameObject> CreateHero(Vector3 at);
        
        void Cleanup();
        void WarmUp();
    }
}