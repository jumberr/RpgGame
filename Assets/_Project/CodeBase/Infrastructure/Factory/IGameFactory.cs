using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        HeroFacade HeroFacade { get; }

        UniTask CreateHero();
        void SetupComponents();
        void Register(ISavedProgressReader progressReader);

        void Cleanup();
    }
}