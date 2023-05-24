using System;
using System.Threading;
using _Project.CodeBase.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic
{
    public abstract class BaseHealthComponent : MonoBehaviour, IHealth
    {
        protected HealthData HealthData;
        
        private CancellationTokenSource _cts;
        private UniTask _regenerationTask;
        private bool _heal;

        public event Action HealthChanged;
        public event Action HealthIncreased;
        public event Action HealthDropped;

        public float Current
        {
            get => HealthData.CurrentHp;
            set
            {
                var healthStatus = GetHealthStatus(value);

                HealthData.CurrentHp = Mathf.Clamp(value, 0, HealthData.MaxHp);
                HealthChanged?.Invoke();
                InvokeEventByStatus(healthStatus);
            }
        }

        public float Max
        {
            get => HealthData.MaxHp;
            set => HealthData.MaxHp = value;
        }

        private void OnDestroy() => 
            _cts?.Cancel();

        public async UniTaskVoid TakeDamage(float damage)
        {
            if (Current <= 0) return;
            Current -= damage;
            _heal = false;

            if (!HealthData.Regeneration) return;
            await WaitForRegeneration();
            await Regenerate();
        }
        
        public void Heal(float health)
        {
            if (Current >= Max) return;
            Current += health;
        }

        public float GetNormalizedHealth() => 
            Current / Max;

        public void Reinitialize() => 
            Current = Max;

        protected void InvokeHealthChanged() =>
            HealthChanged?.Invoke();
        
        private HealthStatus GetHealthStatus(float value)
        {
            if (HealthData.CurrentHp < value) return HealthStatus.Increase;
            return HealthData.CurrentHp > value 
                ? HealthStatus.Drop 
                : HealthStatus.None;
        }

        private void InvokeEventByStatus(HealthStatus status)
        {
            if (status == HealthStatus.Increase)
                HealthIncreased?.Invoke();
            else if (status == HealthStatus.Drop)
                HealthDropped?.Invoke();
        }

        private async UniTask WaitForRegeneration()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(HealthData.RegenerationDelay), cancellationToken: _cts.Token);
            _heal = true;
        }

        private async UniTask Regenerate()
        {
            while (_heal && HealthData.CurrentHp < HealthData.MaxHp)
            {
                Heal(HealthData.RegenerationSpeed);
                await UniTask.Yield(cancellationToken: _cts.Token);
            }
        }
    }
}