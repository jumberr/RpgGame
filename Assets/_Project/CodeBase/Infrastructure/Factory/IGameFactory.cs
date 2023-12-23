using System;
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        event Action<HeroFacade> HeroCreated;
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        HeroFacade HeroFacade { get; }

        UniTask CreateHero();
        void InitializeAI();
        void Register(ISavedProgressReader progressReader);
        void Cleanup();
    }
}