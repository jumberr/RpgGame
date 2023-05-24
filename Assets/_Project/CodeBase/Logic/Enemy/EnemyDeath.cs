using System;
using _Project.CodeBase.Logic.Enemy.FSM;
using _Project.CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyDeath : BaseDeathComponent
    {
        [SerializeField] private EnemyFacade facade;
        [SerializeField] private AIAgent agent;
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyHealthBar healthBar;

        [Header("Dissolve Settings")]
        [SerializeField] private DissolveEffect dissolveEffect;
        [SerializeField] private float deathTime;
        [SerializeField] private float delayTime;
        
        private EnemyObjectPool _objectPool;

        [Inject]
        public void Construct(EnemyObjectPool objectPool) => 
            _objectPool = objectPool;

        protected override async void ProduceHeroDeath()
        {
            agent.ChangeState(AIStateName.Death);
            DeactivateComponents();
            await PlayDeathAnimation();

            await dissolveEffect.ActivateEffect();
            _objectPool.Despawn(facade);
        }

        private void DeactivateComponents()
        {
            agent.NavMeshAgent.ResetPath();
            healthBar.Deactivate();
        }

        private async UniTask PlayDeathAnimation()
        {
            animationController.Animator.PlayDeathAnimation();
            await UniTask.Delay(TimeSpan.FromSeconds(deathTime + delayTime));
            animationController.DisableAnimator();
        }
    }
}