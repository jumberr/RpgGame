using System.IO;
using System.Runtime.Serialization;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.SaveLoad
{
    public class JsonSaveLoadService : SaveLoadService
    {
        protected override string Extension => ".json";

        public JsonSaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory) : base(progressService, gameFactory)
        {
        }

        protected override void Serialize(IPersistentProgressService progressService)
        {
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var json = JsonConvert.SerializeObject(progressService.Progress, settings);
                File.WriteAllText(Path, json);
                Debug.Log("<color=green> Successfully saved! </color>");
            }
            catch (SerializationException e)
            {
                Debug.Log($"<color=red>Failed to serialize. Reason: {e.Message}</color>");
                throw;
            }
        }

        protected override PlayerProgress Deserialize()
        {
            if (!File.Exists(Path)) return null;
            
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var json = File.ReadAllText(Path);
                return JsonConvert.DeserializeObject<PlayerProgress>(json, settings);
            }
            catch (SerializationException e)
            {
                Debug.Log($"<color=red>Failed to deserialize. Reason: {e.Message}</color>");
                throw;
            }
        }
    }
}