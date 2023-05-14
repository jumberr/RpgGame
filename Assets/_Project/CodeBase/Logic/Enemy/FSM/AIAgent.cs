using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.Enemy;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.CodeBase.Logic.Enemy.FSM
{
    public class AIAgent : NightCache, INightRun
    {
        private AIStateMachine _stateMachine;
        private List<AIStateName> _states;
        private EnemyStaticData _enemyStaticData;

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public EnemyAnimator EnemyAnimator { get; private set; }

        [Inject]
        public void Construct(IStaticDataService staticDataService, AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _enemyStaticData = staticDataService.ForEnemy();
            _stateMachine.SetAIAgent(this);
            NavMeshAgent.stoppingDistance = _enemyStaticData.StoppingDistance;
        }

        public void Run() => 
            _stateMachine.Update();

        public void ChangeState(AIStateName state) => 
            _stateMachine.ChangeState(state);

        public float DistanceTo(Vector3 target) => 
            Vector3.Distance(target, CachedTransform.position);

        public void RotateAI(Vector3 heroPosition) =>
            CachedTransform.rotation = Quaternion.Slerp(CachedTransform.rotation, 
                Quaternion.LookRotation(heroPosition - CachedTransform.position),
                _enemyStaticData.RotationModifier * Time.deltaTime);

        public void EnterInitialState() => 
            _stateMachine.ChangeState(_enemyStaticData.InitialState);
    }
}