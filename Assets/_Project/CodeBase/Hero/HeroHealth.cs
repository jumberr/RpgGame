using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        public event Action HealthChanged;
        public event Action ZeroHealth;
        private HealthData _healthData;
        
        public float Current
        {
            get => _healthData.CurrentHP;
            set
            {
                if (_healthData.CurrentHP != value)
                {
                    _healthData.CurrentHP = value;
                    HealthChanged?.Invoke();
                    
                    if (value <= 0)
                        ZeroHealth?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _healthData.MaxHP;
            set => _healthData.MaxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            
            Current -= damage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _healthData = progress.HealthData;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HealthData.CurrentHP = Current;
            progress.HealthData.MaxHP = Max;
        }
    }
}