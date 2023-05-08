using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.StaticData;
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

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        [Inject]
        public void Construct(IStaticDataService staticDataService, AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.SetAIAgent(this);
            EnterInitialState(staticDataService);
        }

        public void Run() => 
            _stateMachine.Update();

        public void ChangeState(AIStateName state) => 
            _stateMachine.ChangeState(state);

        private void EnterInitialState(IStaticDataService staticDataService)
        {
            var initialState = staticDataService.ForEnemy().InitialState;
            _stateMachine.ChangeState(initialState);
        }
    }
}