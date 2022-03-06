using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        public event Action HealthChanged;
        private HealthData _healthData;
        
        public float Current
        {
            get => _healthData.CurrentHp;
            set
            {
                if (_healthData.CurrentHp != value)
                {
                    _healthData.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _healthData.MaxHp;
            set => _healthData.MaxHp = value;
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
            progress.HealthData.CurrentHp = Current;
            progress.HealthData.MaxHp = Max;
        }
    }
}