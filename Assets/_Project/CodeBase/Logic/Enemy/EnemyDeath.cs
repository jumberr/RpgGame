using _Project.CodeBase.Logic.Enemy.FSM;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyDeath : BaseDeathComponent
    {
        [SerializeField] private AIAgent agent;
        [SerializeField] private EnemyAnimationController animationController;

        protected override void ProduceHeroDeath()
        {
            agent.ChangeState(AIStateName.Death);
            animationController.TurnRagdoll();
        }
    }
}