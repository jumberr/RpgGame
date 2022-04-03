using System;
using _Project.CodeBase.Logic.HeroInventory;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HealthData;
        public PositionData PositionData;
        public Inventory Inventory;

        public PlayerProgress(
            HealthData healthData,
            PositionData positionData,
            Inventory inventory)
        {
            HealthData = healthData;
            PositionData = positionData;
            Inventory = inventory;
        }
    }
}