using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class IdleState : IAIState
    {
        private readonly Transform _hero;
        private readonly float _sightDistance;

        public AIStateName StateName => AIStateName.Idle;
        
        public IdleState(HeroFacade.Factory factory, IStaticDataService staticDataService)
        {
            _hero = factory.Instance.CachedTransform;
            _sightDistance = staticDataService.ForEnemy().SightDistance;
        }

        public void Enter(AIAgent agent) => 
            agent.EnemyAnimator.IdleAnimation();

        public void Update(AIAgent agent)
        {
            var playerDirection = _hero.position - agent.CachedTransform.position;
            if (playerDirection.magnitude > _sightDistance) return;

            playerDirection.Normalize();
            var dotProduct = Vector3.Dot(playerDirection, agent.CachedTransform.forward);
            if (dotProduct > 0f) 
                agent.ChangeState(AIStateName.Chase);
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}