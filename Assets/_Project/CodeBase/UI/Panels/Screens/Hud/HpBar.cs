using System;
using System.Threading;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [Header("Shrink Settings")]
        [SerializeField] private Image shrinkBar;
        [SerializeField] private float maxShrink;
        [SerializeField] private float shrinkSpeed;

        private IHealth _health;
        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(HeroFacade.Factory factory)
        {
            _health = factory.Instance.Health;
            _health.HealthChanged += UpdateHealthBar;
            _health.HealthIncreased += Restore;
            _health.HealthDropped += ApplyDamageWrapper;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHealthBar;
            _health.HealthIncreased -= Restore;
            _health.HealthDropped -= ApplyDamageWrapper;
        }

        private void UpdateHealthBar() => 
            SetBarValue(healthBar, _health.GetNormalizedHealth());

        private void Restore() => 
            SetBarValue(shrinkBar, healthBar.fillAmount);

        private void ApplyDamageWrapper() => 
            ApplyDamage().Forget();

        private async UniTaskVoid ApplyDamage()
        {
            PrepareShrink();
            await WaitForShrink();
            ShrinkInjuriesBar().Forget();
        }

        private void PrepareShrink()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
        }

        private async UniTask WaitForShrink() => 
            await UniTask.Delay(TimeSpan.FromSeconds(maxShrink), cancellationToken: _cts.Token);

        private async UniTaskVoid ShrinkInjuriesBar()
        {
            var shrinkFillAmount = shrinkBar.fillAmount;
            var healthFillAmount = healthBar.fillAmount;
            var multiplier = shrinkFillAmount / healthFillAmount;
            
            while (healthFillAmount < shrinkFillAmount)
            {
                shrinkFillAmount -= multiplier *  shrinkSpeed;
                SetBarValue(shrinkBar, shrinkFillAmount);
                await UniTask.Yield(cancellationToken: _cts.Token);
            }
        }

        private void SetBarValue(Image bar, float value) =>
            bar.fillAmount = value;
    }
}