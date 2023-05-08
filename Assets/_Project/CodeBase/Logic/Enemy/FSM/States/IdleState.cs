using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class IdleState : IAIState
    {
        private const float SightDistance = 10f;
        
        private Transform _hero;
        
        public AIStateName StateName => AIStateName.Idle;
        
        [Inject]
        public void Construct(HeroFacade.Factory factory) => 
            _hero = factory.Instance.transform;
        
        public void Enter(AIAgent agent)
        {
        }

        public void Update(AIAgent agent)
        {
            var playerDirection = _hero.position - agent.transform.position;
            if (playerDirection.magnitude > SightDistance) return;

            playerDirection.Normalize();
            var dotProduct = Vector3.Dot(playerDirection, agent.transform.forward);
            if (dotProduct > 0f) 
                agent.ChangeState(AIStateName.Chase);
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}