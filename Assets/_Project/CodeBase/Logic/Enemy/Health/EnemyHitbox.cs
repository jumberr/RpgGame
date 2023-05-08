using _Project.CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyHitbox : MonoBehaviour, IHitBox
    {
        private EnemyHealth _enemyHealth;
        private float _multiplier;
        
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public HitboxName Name { get; private set; }

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            var staticData = staticDataService.ForEnemy();
            _multiplier = staticData.FindHitboxData(Name).Multiplier;
        }

        public void SetupHealth(EnemyHealth health) => 
            _enemyHealth = health;

        public void Hit(float dmg)
        {
            var damage = _multiplier * dmg;
            _enemyHealth.TakeDamage(damage);
            Debug.Log($" [HitBox]: Hit! {damage}");
        }

        public void ToggleRagdoll(bool value) => 
            Rigidbody.isKinematic = value;
    }
}