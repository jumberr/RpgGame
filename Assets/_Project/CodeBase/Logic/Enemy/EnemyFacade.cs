using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private EnemyDeath death;
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimationController animatorController;
        [SerializeField] private EnemyRagdoll ragdoll;
        
        [Inject]
        public void Construct()
        {
            transform.position = new Vector3(Random.Range(-20, 20), 2, Random.Range(-20, 20));
            InitializeEnemy();
        }

        private void InitializeEnemy()
        {
            animatorController.EnableAnimator();
            death.SetHealthComponent(health);
            ragdoll.Setup(health);
        }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}