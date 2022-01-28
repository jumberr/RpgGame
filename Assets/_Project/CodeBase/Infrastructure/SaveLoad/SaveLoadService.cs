using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private string path = Path.Combine($"{Application.persistentDataPath}", "Progress");
        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }
        
        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            Serialize(_progressService);
        }

        public PlayerProgress LoadProgress() => 
            Deserialize();

        private void Serialize(IPersistentProgressService progressService)
        {
            var fs = new FileStream(path, FileMode.OpenOrCreate);
            var bf = new BinaryFormatter();
            try
            {
                bf.Serialize(fs, progressService.Progress);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private PlayerProgress Deserialize()
        {
            if (!File.Exists(path)) return null;
            var fs = new FileStream(path, FileMode.Open);
            try
            {
                var bf = new BinaryFormatter();
                return (PlayerProgress) bf.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}