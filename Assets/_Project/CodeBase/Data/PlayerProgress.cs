using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HealthData;
        public PositionData PositionData;

        public PlayerProgress(
            HealthData healthData,
            PositionData positionData)
        {
            HealthData = healthData;
            PositionData = positionData;
        }
    }
}