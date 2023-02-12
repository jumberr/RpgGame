using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;

namespace _Project.CodeBase.Infrastructure.SaveLoad
{
    public class BinarySaveLoadService : SaveLoadService
    {
        protected override string Extension => ".dat";

        public BinarySaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory) : base(progressService, gameFactory)
        {
        }

        protected override void Serialize(IPersistentProgressService progressService)
        {
            var fs = new FileStream(Path, FileMode.OpenOrCreate);
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

        protected override PlayerProgress Deserialize()
        {
            if (!File.Exists(Path)) return null;
            var fs = new FileStream(Path, FileMode.Open);
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