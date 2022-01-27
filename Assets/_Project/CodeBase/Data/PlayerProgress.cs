using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthData HealthData;

        public PlayerProgress(HealthData healthData) => 
            HealthData = healthData;
    }
}