using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy.Health
{
    public class EnemyHitEffect : NightCache, INightInit
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimator animator;

        public void Init() => 
            health.HealthChanged += PlayHitEffect;

        private void OnDestroy() => 
            health.HealthChanged -= PlayHitEffect;

        private void PlayHitEffect() => 
            animator.PlayHitAnimation();
    }
}