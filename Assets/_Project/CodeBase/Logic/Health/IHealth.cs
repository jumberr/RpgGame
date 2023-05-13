using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        event Action HealthIncreased;
        event Action HealthDropped;
        float Current { get; set; }
        float Max { get; set; }
        UniTaskVoid TakeDamage(float damage);
        void Heal(float health);
        float GetNormalizedHealth();
    }
}