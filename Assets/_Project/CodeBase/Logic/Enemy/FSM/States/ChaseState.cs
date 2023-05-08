using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy.FSM.States
{
    public class ChaseState : IAIState
    {
        private const float MaxDistance = 1f;
        private const float MaxTime = 1f;
        
        private Transform _hero;
        private float _timer;

        public AIStateName StateName => AIStateName.Chase;

        [Inject]
        public void Construct(HeroFacade.Factory factory) => 
            _hero = factory.Instance.transform;

        public void Enter(AIAgent agent)
        {
        }

        public void Update(AIAgent agent)
        {
            _timer -= Time.deltaTime;
            if (!agent.NavMeshAgent.hasPath)
                agent.NavMeshAgent.destination = _hero.position;

            if (!(_timer < 0f)) return;
            var direction =  _hero.position - agent.NavMeshAgent.destination;
            direction.y = 0f;
            if (direction.sqrMagnitude > MaxDistance * MaxDistance && agent.NavMeshAgent.pathStatus != NavMeshPathStatus.PathPartial) 
                agent.NavMeshAgent.destination = _hero.position;
            _timer = MaxTime;
        }

        public void Exit(AIAgent agent)
        {
        }
    }
}