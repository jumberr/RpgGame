using System;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public abstract class BaseDeathComponent : MonoBehaviour
    {
        private IHealth _health;
        private bool _isDead;
        
        public event Action ZeroHealth;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        public void SetHealthComponent(IHealth health) => 
            _health = health;

        protected abstract void ProduceHeroDeath();

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            ZeroHealth?.Invoke();
            _isDead = true;
            ProduceHeroDeath();
        }
    }
}