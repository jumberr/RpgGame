using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyDeath : BaseDeathComponent
    {
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyAnimationController animationController;

        protected override void ProduceHeroDeath()
        {
            movement.Disable();
            animationController.TurnRagdoll();
        }
    }
}