using System.Collections.Generic;
using _Project.CodeBase.Logic.Enemy;
using _Project.CodeBase.Logic.Enemy.Health;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class MeleeWeapon : BaseMeleeAttack
    {
        private readonly List<Transform> _dealtDamage = new List<Transform>();

        [UsedImplicitly]
        public void OnAttack()
        {
            _dealtDamage.Clear();
            
            for (var i = 0; i < Hit(); i++)
            {
                var hitTransform = Hits[i].transform;
                if (hitTransform.TryGetComponent<IHitBox>(out var hitBox))
                {
                    var enemyRoot = hitTransform.root;
                    if (_dealtDamage.Contains(enemyRoot)) continue;

                    hitBox.Hit(Damage);
                    SpawnBloodParticles(enemyRoot, hitTransform, i);
                    _dealtDamage.Add(enemyRoot);
                }
            }
        }

        private void SpawnBloodParticles(Transform enemyRoot, Transform hitTransform, int index)
        {
            if (!enemyRoot.TryGetComponent<EnemyHitEffect>(out var hitEffect)) return;
            var closest = Physics.ClosestPoint(SphereCollider.center, Hits[index], hitTransform.position, hitTransform.rotation);
            hitEffect.SpawnBloodParticles(closest).Forget();
        }
    }
}