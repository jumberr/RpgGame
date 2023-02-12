using System;
using _Project.CodeBase.Logic.Inventory;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HealthData;
        public GameObjectData GameObjectData;
        public MapData MapData;
        public Inventory Inventory;
        public SettingsData Settings;

        public PlayerProgress(
            HealthData healthData,
            GameObjectData gameObjectData,
            MapData mapData,
            Inventory inventory,
            SettingsData settings)
        {
            HealthData = healthData;
            GameObjectData = gameObjectData;
            MapData = mapData;
            Inventory = inventory;
            Settings = settings;
        }
    }
}