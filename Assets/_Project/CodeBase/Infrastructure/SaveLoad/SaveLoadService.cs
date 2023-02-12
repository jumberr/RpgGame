using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Progress = "Progress";
        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        protected string Path;
        
        protected virtual string Extension => "";

        protected SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            InitializePath();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            Serialize(_progressService);
        }

        public PlayerProgress LoadProgress() => 
            Deserialize();

        protected virtual void Serialize(IPersistentProgressService progressService) { }

        protected virtual PlayerProgress Deserialize() => 
            null;

        private void InitializePath() =>
            Path = System.IO.Path.Combine(Application.persistentDataPath, $"{Progress}{Extension}");
    }
}