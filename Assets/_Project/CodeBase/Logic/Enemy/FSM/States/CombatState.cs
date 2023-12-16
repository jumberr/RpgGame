using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.StaticData.Enemy;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class CombatState : IAIState
    {
        private readonly Transform _hero;
        private readonly EnemyStaticData _enemyConfig;
        private float _attackTimer;

        public AIStateName StateName => AIStateName.Combat;

        public CombatState(HeroFacade.Factory factory, IStaticDataService staticDataService)
        {
            _hero = factory.Instance.CachedTransform;
            _enemyConfig = staticDataService.ForEnemy();
        }
        
        public void Enter(AIAgent agent)
        {
        }

        public void Update(AIAgent agent)
        {
            var position = _hero.position;
            
            agent.RotateAI(position);

            var distance = agent.DistanceTo(position);
            if (distance <= _enemyConfig.AttackRange)
            {
                if (_attackTimer <= 0)
                {
                    agent.EnemyAnimator.PlayAttackAnimation();
                    _attackTimer = _enemyConfig.AttackCooldown;
                }
            }
            else
            {
                agent.ChangeState(AIStateName.Chase);
                return;
            }

            _attackTimer -= Time.deltaTime;
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}