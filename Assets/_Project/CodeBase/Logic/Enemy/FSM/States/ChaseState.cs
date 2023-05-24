using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.StaticData.Enemy;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class ChaseState : IAIState
    {
        private readonly Transform _hero;
        private readonly EnemyStaticData _enemyStaticData;
        private float _destinationTimer;

        public AIStateName StateName => AIStateName.Chase;

        public ChaseState(HeroFacade.Factory factory, IStaticDataService staticDataService)
        {
            _hero = factory.Instance.CachedTransform;
            _enemyStaticData = staticDataService.ForEnemy();
        }

        public void Enter(AIAgent agent)
        {
        }

        public void Update(AIAgent agent)
        {
            if (!agent.NavMeshAgent.enabled) return;

            var heroPosition = _hero.position;

            if (_destinationTimer <= 0)
            {
                _destinationTimer = _enemyStaticData.DestinationCooldown;
                agent.NavMeshAgent.SetDestination(heroPosition);
            }
            
            var distance = agent.DistanceTo(heroPosition);
            if (distance <= _enemyStaticData.AttackRange)
            {
                agent.ChangeState(AIStateName.Combat);
                return;
            }

            if (distance > _enemyStaticData.SightDistance)
            {
                agent.ChangeState(AIStateName.Idle);
                return;
            }

            _destinationTimer -= Time.deltaTime;

            agent.RotateAI(heroPosition);
            agent.EnemyAnimator.PlayMoveAnimation(agent.NavMeshAgent.velocity.magnitude / agent.NavMeshAgent.speed);
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}