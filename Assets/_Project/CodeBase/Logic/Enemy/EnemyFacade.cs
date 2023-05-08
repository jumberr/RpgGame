using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAnimationController animatorController;
        [SerializeField] private EnemyRagdoll ragdoll;

        private HeroFacade _player;

        [Inject]
        public void Construct(HeroFacade.Factory heroFactory)
        {
            _player = heroFactory.Instance;
            InitializeEnemy();
        }

        private void InitializeEnemy()
        {
            movement.Initialize(_player.transform);
            animatorController.TurnAnimator();
            ragdoll.Setup(health);
        }

        public class Factory : PlaceholderFactory<EnemyFacade>
        {
        }
    }
}