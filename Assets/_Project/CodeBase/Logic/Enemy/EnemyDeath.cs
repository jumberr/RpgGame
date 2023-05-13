using _Project.CodeBase.Logic.Enemy.FSM;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyDeath : BaseDeathComponent
    {
        [SerializeField] private AIAgent agent;
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private DissolveEffect dissolveEffect;

        protected override void ProduceHeroDeath()
        {
            agent.ChangeState(AIStateName.Death);
            animationController.DisableAnimator();
            dissolveEffect.ActivateEffect().Forget();
            
        }
    }
}