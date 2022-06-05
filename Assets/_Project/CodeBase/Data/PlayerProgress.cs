using System;
using _Project.CodeBase.Logic.Inventory;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HealthData;
        public PositionData PositionData;
        public Inventory Inventory;
        public SettingsData Settings;

        public PlayerProgress(
            HealthData healthData,
            PositionData positionData,
            Inventory inventory,
            SettingsData settings)
        {
            HealthData = healthData;
            PositionData = positionData;
            Inventory = inventory;
            Settings = settings;
        }
    }
}