using System;
using _Project.CodeBase.Data;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public abstract class BaseHealthComponent : MonoBehaviour, IHealth
    {
        protected HealthData HealthData;
        
        public event Action HealthChanged;

        public float Current
        {
            get => HealthData.CurrentHp;
            set
            {
                if (HealthData.CurrentHp.Equals(value)) return;
                HealthData.CurrentHp = value;
                HealthChanged?.Invoke();
            }
        }

        public float Max
        {
            get => HealthData.MaxHp;
            set => HealthData.MaxHp = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;
            Current -= damage;
        }

        protected void InvokeHealthChanged() => 
            HealthChanged?.Invoke();
    }
}