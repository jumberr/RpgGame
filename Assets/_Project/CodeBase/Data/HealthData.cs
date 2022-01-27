using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class HealthData
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}