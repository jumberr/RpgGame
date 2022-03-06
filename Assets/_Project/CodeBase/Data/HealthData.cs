using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class HealthData
    {
        public float CurrentHp;
        public float MaxHp;

        public void ResetHp() => CurrentHp = MaxHp;
    }
}