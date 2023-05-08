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
        public void Construct() => 
            InitializeEnemy();

        private void InitializeEnemy()
        {
            animatorController.TurnAnimator();
            death.SetHealthComponent(health);
            ragdoll.Setup(health);
        }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}