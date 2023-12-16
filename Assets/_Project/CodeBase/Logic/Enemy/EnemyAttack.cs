using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAttack : BaseMeleeAttack
    {
        [SerializeField] private LayerMask layerMask;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            var enemy = staticDataService.ForEnemy();
            Construct(layerMask, enemy.Damage);
        }

        [UsedImplicitly]
        public void Attack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                if (Hits[i].transform.TryGetComponent<HeroHealth>(out var health)) 
                    health.TakeDamage(Damage).Forget();
            }
        }
    }
}