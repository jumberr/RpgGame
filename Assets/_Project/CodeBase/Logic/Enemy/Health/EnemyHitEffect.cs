using System;
using Cysharp.Threading.Tasks;
using NTC.Global.Cache;
using NTC.Global.Pool;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy.Health
{
    public class EnemyHitEffect : NightCache, INightInit
    {
        private const float TimeDestroyFX = 0.2f;

        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private GameObject bloodParticles;

        public void Init() => 
            health.HealthChanged += animator.PlayHitAnimation;

        private void OnDestroy() => 
            health.HealthChanged -= animator.PlayHitAnimation;

        public async UniTask SpawnBloodParticles(RaycastHit hit)
        {
            var fx = NightPool.Spawn(bloodParticles, hit.point, Quaternion.LookRotation(hit.normal));
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            NightPool.Despawn(fx);
        }
        
        public async UniTask SpawnBloodParticles(Vector3 pos)
        {
            var fx = NightPool.Spawn(bloodParticles, pos, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            NightPool.Despawn(fx);
        }
    }
}